using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Web.WebView2.Core;

namespace MarkdownThing
{
    public partial class Form1
    {
        private const int EmGetFirstVisibleLine = 0x00CE;
        private const int EmLineScroll = 0x00B6;

        private bool _suppressScrollSync;
        private int _pendingPreviewScrollLine = -1;
        private int _lastPreviewScrollLine;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void InitializeScrollSync()
        {
            markdownEditor.Scroll += MarkdownEditor_Scroll;
        }

        private void InitializeWebViewScrollSync()
        {
            if (webView.CoreWebView2 == null)
                return;

            webView.CoreWebView2.NavigationCompleted += WebView_NavigationCompleted;
            webView.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;
        }

        private bool ShouldPreservePreviewScroll() =>
            _isEditMode && !splitContainer.Panel1Collapsed;

        private async void WebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess || webView.CoreWebView2 == null)
                return;

            await InjectPreviewScrollSyncScriptAsync();

            if (_pendingPreviewScrollLine >= 0)
            {
                await ScrollPreviewToSourceLineAsync(_pendingPreviewScrollLine);
                _pendingPreviewScrollLine = -1;
            }
        }

        private void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                using var doc = JsonDocument.Parse(e.WebMessageAsJson);
                if (!doc.RootElement.TryGetProperty("type", out var typeProp))
                    return;

                var type = typeProp.GetString();
                if (type == "previewScroll" &&
                    doc.RootElement.TryGetProperty("sourceLine", out var lineProp) &&
                    lineProp.TryGetInt32(out var sourceLine))
                {
                    _lastPreviewScrollLine = sourceLine;

                    if (_isEditMode && !splitContainer.Panel1Collapsed)
                        SyncEditorToPreview(sourceLine);
                    return;
                }

                if (type == "previewScroll" &&
                    doc.RootElement.TryGetProperty("ratio", out var ratioProp))
                {
                    var ratio = ratioProp.GetDouble();
                    _lastPreviewScrollLine = SourceLineFromRatio(ratio);

                    if (_isEditMode && !splitContainer.Panel1Collapsed)
                        ScrollEditorToRatio(ratio);
                }
            }
            catch
            {
                // Ignore malformed scroll messages from the preview page.
            }
        }

        private void MarkdownEditor_Scroll(object? sender, EventArgs e) =>
            SyncPreviewToEditor();

        private async void SyncPreviewToEditor()
        {
            if (_suppressScrollSync || !_webViewInitialized || !_isEditMode || splitContainer.Panel1Collapsed)
                return;

            if (webView.CoreWebView2 == null)
                return;

            var sourceLine = GetFirstVisibleEditorLine();
            _lastPreviewScrollLine = sourceLine;

            try
            {
                await ScrollPreviewToSourceLineAsync(sourceLine);
            }
            catch
            {
                // Preview scroll sync is best-effort during live editing.
            }
        }

        private void SyncEditorToPreview(int sourceLine)
        {
            if (_suppressScrollSync || !_isEditMode || splitContainer.Panel1Collapsed)
                return;

            _suppressScrollSync = true;
            try
            {
                ScrollEditorToLine(sourceLine);
            }
            finally
            {
                _suppressScrollSync = false;
            }
        }

        private void ScrollEditorToLine(int targetLine)
        {
            var maxLine = Math.Max(0, GetEditorLineCount() - 1);
            targetLine = Math.Clamp(targetLine, 0, maxLine);
            var delta = targetLine - GetFirstVisibleEditorLine();

            if (delta != 0)
                SendMessage(markdownEditor.Handle, EmLineScroll, IntPtr.Zero, (IntPtr)delta);
        }

        private void ScrollEditorToRatio(double ratio)
        {
            var totalLines = GetEditorLineCount();
            var visibleLines = GetVisibleEditorLineCount();
            var maxFirstLine = Math.Max(0, totalLines - visibleLines);
            var targetLine = (int)Math.Round(Math.Clamp(ratio, 0, 1) * maxFirstLine);
            ScrollEditorToLine(targetLine);
        }

        private int SourceLineFromRatio(double ratio)
        {
            var totalLines = GetEditorLineCount();
            var visibleLines = GetVisibleEditorLineCount();
            var maxFirstLine = Math.Max(0, totalLines - visibleLines);
            return (int)Math.Round(Math.Clamp(ratio, 0, 1) * maxFirstLine);
        }

        private int GetFirstVisibleEditorLine() =>
            SendMessage(markdownEditor.Handle, EmGetFirstVisibleLine, IntPtr.Zero, IntPtr.Zero).ToInt32();

        private int GetEditorLineCount()
        {
            var text = markdownEditor.Text;
            if (text.Length == 0)
                return 1;

            return text.Split('\n').Length;
        }

        private int GetVisibleEditorLineCount()
        {
            var lineHeight = TextRenderer.MeasureText("Ay", markdownEditor.Font).Height;
            return Math.Max(1, markdownEditor.ClientSize.Height / Math.Max(1, lineHeight));
        }

        private static async Task InjectPreviewScrollSyncScriptAsync(CoreWebView2? core)
        {
            if (core == null)
                return;

            const string script = """
                (function() {
                    if (window.__mdScrollSyncInstalled) return;
                    window.__mdScrollSyncInstalled = true;

                    function getHeadings() {
                        return Array.from(document.querySelectorAll('[data-source-line]'))
                            .map(function(el) {
                                return {
                                    line: parseInt(el.getAttribute('data-source-line'), 10),
                                    top: el.offsetTop
                                };
                            })
                            .filter(function(h) { return !isNaN(h.line); })
                            .sort(function(a, b) { return a.line - b.line; });
                    }

                    window.__mdScrollToSourceLine = function(sourceLine, totalLines) {
                        window.__mdScrollSyncFromHost = true;
                        var headings = getHeadings();
                        var line = Math.max(0, sourceLine);

                        if (headings.length === 0) {
                            var max = Math.max(1, document.documentElement.scrollHeight - document.documentElement.clientHeight);
                            var ratio = Math.max(0, Math.min(1, line / Math.max(1, totalLines - 1)));
                            document.documentElement.scrollTop = ratio * max;
                        } else {
                            var start = headings[0];
                            var next = null;
                            for (var i = 0; i < headings.length; i++) {
                                if (headings[i].line <= line) {
                                    start = headings[i];
                                    next = headings[i + 1] || null;
                                } else {
                                    break;
                                }
                            }

                            var endLine = next ? next.line - 1 : Math.max(start.line, totalLines - 1);
                            var t = endLine > start.line
                                ? Math.max(0, Math.min(1, (line - start.line) / (endLine - start.line)))
                                : 0;
                            var endTop = next ? next.top : document.body.scrollHeight;
                            var scrollTop = start.top + t * (endTop - start.top);
                            document.documentElement.scrollTop = scrollTop;
                        }

                        requestAnimationFrame(function() { window.__mdScrollSyncFromHost = false; });
                    };

                    window.__mdGetScrollSourceLine = function(totalLines) {
                        var headings = getHeadings();
                        var scrollTop = window.scrollY;

                        if (headings.length === 0) {
                            var max = Math.max(1, document.documentElement.scrollHeight - document.documentElement.clientHeight);
                            var ratio = max <= 0 ? 0 : scrollTop / max;
                            return Math.round(ratio * Math.max(0, totalLines - 1));
                        }

                        var start = headings[0];
                        var next = null;
                        for (var i = 0; i < headings.length; i++) {
                            if (headings[i].top <= scrollTop + 8) {
                                start = headings[i];
                                next = headings[i + 1] || null;
                            } else {
                                break;
                            }
                        }

                        if (!next) {
                            return start.line;
                        }

                        var sectionStart = start.top;
                        var sectionEnd = next.top;
                        if (sectionEnd <= sectionStart) {
                            return start.line;
                        }

                        var t = Math.max(0, Math.min(1, (scrollTop - sectionStart) / (sectionEnd - sectionStart)));
                        var lineSpan = next.line - start.line;
                        return Math.round(start.line + t * lineSpan);
                    };

                    var timer = null;
                    window.addEventListener('scroll', function() {
                        if (window.__mdScrollSyncFromHost) return;
                        clearTimeout(timer);
                        timer = setTimeout(function() {
                            var totalLines = window.__mdEditorLineCount || 1;
                            if (getHeadings().length > 0) {
                                var sourceLine = window.__mdGetScrollSourceLine(totalLines);
                                chrome.webview.postMessage(JSON.stringify({
                                    type: 'previewScroll',
                                    sourceLine: sourceLine
                                }));
                            } else {
                                var el = document.documentElement;
                                var max = Math.max(1, el.scrollHeight - el.clientHeight);
                                var ratio = max <= 0 ? 0 : el.scrollTop / max;
                                chrome.webview.postMessage(JSON.stringify({
                                    type: 'previewScroll',
                                    ratio: ratio
                                }));
                            }
                        }, 16);
                    }, { passive: true });
                })();
                """;

            await core.ExecuteScriptAsync(script);
        }

        private Task InjectPreviewScrollSyncScriptAsync() =>
            InjectPreviewScrollSyncScriptAsync(webView.CoreWebView2);

        private async Task ScrollPreviewToSourceLineAsync(int sourceLine)
        {
            if (webView.CoreWebView2 == null)
                return;

            var totalLines = GetEditorLineCount();
            await webView.CoreWebView2.ExecuteScriptAsync(
                $"window.__mdEditorLineCount = {totalLines};");

            await webView.CoreWebView2.ExecuteScriptAsync(
                $"window.__mdScrollToSourceLine({sourceLine}, {totalLines});");
        }
    }
}
