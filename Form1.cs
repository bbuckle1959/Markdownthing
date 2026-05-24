using MDConvertLib;
using Microsoft.Web.WebView2.Core;

namespace MarkdownThing
{
    public partial class Form1 : Form
    {
        private const int MaxRecentFiles = 10;
        private static readonly string RecentFilesPath = AppDataPaths.RecentFilesFile;

        private string? _currentFilePath;
        private string? _currentMarkdown;
        private string? _currentHtml;
        private string? _savedMarkdown;
        private bool _isEditMode;
        private bool _isModified;
        private bool _webViewInitialized;
        private bool _formShown;
        private bool _hasOpenDocument;
        private string? _pendingFileToOpen;
        private System.Windows.Forms.Timer? _previewTimer;
        private readonly MarkdownConverter _converter;
        private readonly List<string> _recentFiles = [];
        private readonly AppSettings _settings;

        public Form1(string? fileToOpen = null)
        {
            InitializeComponent();
            AppIcons.ApplyTo(this);
            _converter = new MarkdownConverter();
            _settings = AppSettings.Load();
            InitializeExtraUi();
            InitializeEnhancements();
            InitializeScrollSync();
            InitializeEditorUndo();

            _previewTimer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            _previewTimer.Tick += PreviewTimer_Tick;

            LoadRecentFiles();
            UpdateRecentFilesMenu();
            ConfigureWebView();
            InitializeWebViewAsync();

            _pendingFileToOpen = string.IsNullOrWhiteSpace(fileToOpen) ? null : fileToOpen;
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object? sender, EventArgs e)
        {
            Shown -= Form1_Shown;
            _formShown = true;

            TryLoadPendingFile();

            if (string.IsNullOrEmpty(_currentMarkdown) && string.IsNullOrWhiteSpace(_pendingFileToOpen))
                ShowWelcomeIfNeeded();

            UpdatePreview();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData is (Keys.Control | Keys.Oemplus) or (Keys.Control | Keys.Add))
            {
                AdjustPreviewZoom(0.1);
                return true;
            }

            if (keyData is (Keys.Control | Keys.OemMinus) or (Keys.Control | Keys.Subtract))
            {
                AdjustPreviewZoom(-0.1);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D0))
            {
                SetPreviewZoom(1.0);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Z))
            {
                if (_isEditMode)
                {
                    PerformEditorUndo();
                    return true;
                }
            }

            if (_isEditMode && markdownEditor.Focused)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.B:
                        BoldButton_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.I:
                        ItalicButton_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.D1:
                    case Keys.Control | Keys.NumPad1:
                        Heading1MenuItem_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.D2:
                    case Keys.Control | Keys.NumPad2:
                        Heading2MenuItem_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.D3:
                    case Keys.Control | Keys.NumPad3:
                        Heading3MenuItem_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.D4:
                    case Keys.Control | Keys.NumPad4:
                        Heading4MenuItem_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.D5:
                    case Keys.Control | Keys.NumPad5:
                        Heading5MenuItem_Click(this, EventArgs.Empty);
                        return true;
                    case Keys.Control | Keys.D6:
                    case Keys.Control | Keys.NumPad6:
                        Heading6MenuItem_Click(this, EventArgs.Empty);
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OpenFileFromCommandLine(string filePath)
        {
            if (File.Exists(filePath) && string.Equals(Path.GetExtension(filePath), AppConstants.MarkdownExtension, StringComparison.OrdinalIgnoreCase))
            {
                LoadMarkdownFile(filePath);
            }
        }

        private void TryLoadPendingFile()
        {
            if (string.IsNullOrWhiteSpace(_pendingFileToOpen))
                return;

            var path = _pendingFileToOpen;
            _pendingFileToOpen = null;
            OpenFileFromCommandLine(path);
        }

        private void ConfigureWebView()
        {
            Directory.CreateDirectory(AppDataPaths.WebView2Folder);
            webView.CreationProperties = new Microsoft.Web.WebView2.WinForms.CoreWebView2CreationProperties
            {
                UserDataFolder = AppDataPaths.WebView2Folder
            };
        }

        private async void InitializeWebViewAsync()
        {
            try
            {
                var env = await CoreWebView2Environment.CreateAsync(
                    browserExecutableFolder: null,
                    userDataFolder: AppDataPaths.WebView2Folder);
                await webView.EnsureCoreWebView2Async(env);
                _webViewInitialized = true;
                InitializeWebViewScrollSync();
                toolStripStatusLabel.Text = "Ready - Open a Markdown file to get started";
                TryLoadPendingFile();
                UpdatePreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize WebView2: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PromptSaveChanges()) return;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadMarkdownFile(openFileDialog.FileName);
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e) =>
            CreateNewDocument();

        private void CreateNewDocument()
        {
            if (!PromptSaveChanges())
                return;

            _currentFilePath = null;
            _currentMarkdown = "";
            _savedMarkdown = "";
            _isModified = false;
            _hasOpenDocument = true;
            SetEditorText("");

            SetEditMode(true);
            UpdatePreview();
            UpdateTitle();
            UpdateMenuState();
            UpdateWordCount();
            toolStripStatusLabel.Text = "New document — press Ctrl+S to save";
        }

        private void LoadMarkdownFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                _currentFilePath = filePath;
                _currentMarkdown = content;
                _savedMarkdown = content;
                _isModified = false;
                _hasOpenDocument = true;
                SetEditorText(_currentMarkdown);

                UpdateTitle();
                toolStripStatusLabel.Text = $"Loaded: {filePath}";
                UpdateMenuState();
                UpdateWordCount();

                AddToRecentFiles(filePath);
                UpdatePreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error loading file";
            }
        }

        private void UpdateTitle()
        {
            var fileName = _currentFilePath != null ? Path.GetFileName(_currentFilePath) : "Untitled";
            var modified = _isModified ? "*" : "";
            var mode = _isEditMode ? "Edit" : "Preview";
            Text = $"MarkdownThing - {fileName}{modified} [{mode}]";
        }

        private void UpdateMenuState()
        {
            exportToolStripMenuItem.Enabled = _hasOpenDocument;
            saveToolStripMenuItem.Enabled = _isModified;
            saveAsToolStripMenuItem.Enabled = _hasOpenDocument;

            undoToolStripMenuItem.Enabled = _isEditMode && CanEditorUndo;
            cutToolStripMenuItem.Enabled = _isEditMode;
            copyToolStripMenuItem.Enabled = _isEditMode;
            pasteToolStripMenuItem.Enabled = _isEditMode;
            selectAllToolStripMenuItem.Enabled = _isEditMode;
            UpdateEnhancementMenuState();
        }

        private void UpdateWordCount()
        {
            var text = _currentMarkdown ?? "";
            var charCount = text.Length;
            var lineCount = text.Length == 0 ? 0 : text.Split('\n').Length;
            var wordCount = text.Length == 0 ? 0 :
                text.Split([' ', '\t', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries).Length;
            toolStripWordCountLabel.Text = $"Words: {wordCount}  Lines: {lineCount}  Chars: {charCount}";
        }

        private void SetEditMode(bool editMode)
        {
            _isEditMode = editMode;
            splitContainer.Panel1Collapsed = !editMode;
            formattingToolStrip.Visible = editMode;
            toggleEditModeToolStripMenuItem.Checked = editMode;
            UpdateTitle();
            UpdateMenuState();

            if (editMode)
            {
                BeginInvoke(() =>
                {
                    EqualizeSplitPanes();
                    ScrollEditorToLine(_lastPreviewScrollLine);
                    SyncPreviewToEditor();
                    FocusEditorWithoutSelection();
                });
            }
            else
            {
                _lastPreviewScrollLine = GetFirstVisibleEditorLine();
            }
        }

        private void EqualizeSplitPanes()
        {
            if (splitContainer.Width <= 0)
                return;

            var available = splitContainer.Width - splitContainer.SplitterWidth;
            var half = available / 2;
            var min = splitContainer.Panel1MinSize;
            var max = Math.Max(min, available - splitContainer.Panel2MinSize);
            splitContainer.SplitterDistance = Math.Clamp(half, min, max);
        }

        private void FocusEditorWithoutSelection()
        {
            markdownEditor.Focus();
            var line = GetFirstVisibleEditorLine();
            var charIndex = markdownEditor.GetFirstCharIndexFromLine(line);
            if (charIndex < 0)
                charIndex = markdownEditor.Text.Length;

            markdownEditor.SelectionStart = charIndex;
            markdownEditor.SelectionLength = 0;
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
                PerformEditorUndo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                markdownEditor.Cut();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                markdownEditor.Copy();
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                markdownEditor.Paste();
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                markdownEditor.SelectAll();
            }
        }

        private void ToggleEditModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_isEditMode && !_hasOpenDocument)
            {
                CreateNewDocument();
                return;
            }

            SetEditMode(!_isEditMode);
        }

        private void MarkdownEditor_TextChanged(object sender, EventArgs e)
        {
            RecordEditorChangeForUndo();

            _currentMarkdown = markdownEditor.Text;
            _isModified = _currentMarkdown != _savedMarkdown;
            UpdateTitle();
            UpdateMenuState();
            UpdateWordCount();
            UpdateCaretPosition();

            _previewTimer?.Stop();
            _previewTimer?.Start();
        }

        private void PreviewTimer_Tick(object? sender, EventArgs e)
        {
            _previewTimer?.Stop();
            UpdatePreview();
        }

        private void FlushPreview()
        {
            _previewTimer?.Stop();
            if (_isEditMode)
                _currentMarkdown = markdownEditor.Text;
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (!_webViewInitialized || !_formShown)
                return;

            if (string.IsNullOrEmpty(_currentMarkdown))
            {
                var emptyHtml = _hasOpenDocument
                    ? "<p></p>"
                    : "<p><em>Open a Markdown file (Ctrl+O), create a new document (Ctrl+N), or press Ctrl+E to enter edit mode.</em></p>";
                _currentHtml = MarkdownConverter.WrapInHtmlDocument(
                    emptyHtml,
                    _settings.PreviewTheme, _settings.DarkPreview);
            }
            else
            {
                _currentHtml = _converter.ConvertToHtmlDocument(_currentMarkdown, _settings.PreviewTheme, _settings.DarkPreview);
            }

            if (ShouldPreservePreviewScroll())
                _pendingPreviewScrollLine = GetFirstVisibleEditorLine();

            webView.NavigateToString(_currentHtml);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFilePath != null)
            {
                SaveFile(_currentFilePath);
            }
            else
            {
                SaveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Markdown files (*.md)|*.md|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "md";
            saveFileDialog.Title = "Save Markdown File";

            if (_currentFilePath != null)
            {
                saveFileDialog.FileName = Path.GetFileName(_currentFilePath);
            }
            else
            {
                saveFileDialog.FileName = "Untitled.md";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveFileDialog.FileName);
            }
        }

        private void SaveFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, _currentMarkdown ?? "");
                _currentFilePath = filePath;
                _savedMarkdown = _currentMarkdown;
                _isModified = false;
                UpdateTitle();
                UpdateMenuState();
                AddToRecentFiles(filePath);
                toolStripStatusLabel.Text = $"Saved: {filePath}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Save Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool PromptSaveChanges()
        {
            if (!_isModified) return true;

            var result = MessageBox.Show(
                "Do you want to save changes to the current document?",
                "Unsaved Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Cancel) return false;
            if (result == DialogResult.Yes)
            {
                SaveToolStripMenuItem_Click(this, EventArgs.Empty);
                return !_isModified;
            }
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PromptSaveChanges())
            {
                e.Cancel = true;
                return;
            }

            SaveWindowSettings();
            _findDialog?.Close();
        }

        private void Form1_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0 &&
                    string.Equals(Path.GetExtension(files[0]), AppConstants.MarkdownExtension, StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0 &&
                    string.Equals(Path.GetExtension(files[0]), AppConstants.MarkdownExtension, StringComparison.OrdinalIgnoreCase))
                {
                    if (PromptSaveChanges())
                    {
                        LoadMarkdownFile(files[0]);
                    }
                }
            }
        }

        private async void ExportToPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlushPreview();

            if (!TryBeginExport(out var exportError))
            {
                MessageBox.Show(exportError, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            if (_currentFilePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(_currentFilePath) + ".pdf";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                await ExportToPdfAsync(saveFileDialog.FileName);
            }
        }

        private async void ExportToPngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlushPreview();

            if (!TryBeginExport(out var exportError))
            {
                MessageBox.Show(exportError, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog.Filter = "PNG images (*.png)|*.png";
            saveFileDialog.DefaultExt = "png";
            if (_currentFilePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(_currentFilePath) + ".png";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                await ExportToPngAsync(saveFileDialog.FileName);
            }
        }

        private void ExportToHtmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlushPreview();

            if (!TryBeginExport(out var exportError))
            {
                MessageBox.Show(exportError, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog.Filter = "HTML files (*.html)|*.html";
            saveFileDialog.DefaultExt = "html";
            if (_currentFilePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(_currentFilePath) + ".html";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToHtml(saveFileDialog.FileName);
            }
        }

        private void ExportToHtml(string outputPath)
        {
            try
            {
                File.WriteAllText(outputPath, _currentHtml!);
                _lastHtmlExportPath = outputPath;
                if (openHtmlInBrowserMenuItem != null)
                    openHtmlInBrowserMenuItem.Enabled = true;
                toolStripStatusLabel.Text = $"HTML exported: {outputPath}";
                var openNow = MessageBox.Show(
                    $"HTML saved to:\n{outputPath}\n\nOpen it in your browser now?",
                    "Export complete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (openNow == DialogResult.Yes)
                    OpenHtmlInBrowser_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to HTML: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error exporting HTML";
            }
        }

        private void ExportToWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlushPreview();

            if (!TryBeginExport(out var exportError))
            {
                MessageBox.Show(exportError, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog.Filter = "Word documents (*.docx)|*.docx";
            saveFileDialog.DefaultExt = "docx";
            if (_currentFilePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(_currentFilePath) + ".docx";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToWord(saveFileDialog.FileName);
            }
        }

        private void ExportToWord(string outputPath)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                toolStripStatusLabel.Text = "Generating Word document...";

                _converter.ConvertToWordDocument(_currentMarkdown!, outputPath);

                toolStripStatusLabel.Text = $"Word document exported: {outputPath}";
                MessageBox.Show($"Word document successfully exported to:\n{outputPath}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Word: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error exporting Word document";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ExportToTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlushPreview();

            if (!TryBeginExport(out var exportError))
            {
                MessageBox.Show(exportError, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            if (_currentFilePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(_currentFilePath) + ".txt";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToText(saveFileDialog.FileName);
            }
        }

        private void ExportToText(string outputPath)
        {
            try
            {
                var plainText = _converter.ConvertToPlainText(_currentMarkdown!);
                File.WriteAllText(outputPath, plainText);
                toolStripStatusLabel.Text = $"Text file exported: {outputPath}";
                MessageBox.Show($"Text file successfully exported to:\n{outputPath}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to text: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error exporting text file";
            }
        }

        private async Task ExportToPdfAsync(string outputPath)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                exportToolStripMenuItem.Enabled = false;
                await _converter.ConvertToPdfAsync(_currentHtml!, outputPath, _settings.ToPdfOptions(),
                    msg => toolStripStatusLabel.Text = msg);

                toolStripStatusLabel.Text = $"PDF exported: {outputPath}";
                MessageBox.Show($"PDF successfully exported to:\n{outputPath}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error exporting PDF";
            }
            finally
            {
                Cursor = Cursors.Default;
                UpdateMenuState();
            }
        }

        private async Task ExportToPngAsync(string outputPath)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                exportToolStripMenuItem.Enabled = false;
                await _converter.ConvertToPngAsync(_currentHtml!, outputPath, null,
                    msg => toolStripStatusLabel.Text = msg);

                toolStripStatusLabel.Text = $"PNG exported: {outputPath}";
                MessageBox.Show($"PNG successfully exported to:\n{outputPath}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PNG: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error exporting PNG";
            }
            finally
            {
                Cursor = Cursors.Default;
                UpdateMenuState();
            }
        }

        private void LoadRecentFiles()
        {
            try
            {
                if (File.Exists(RecentFilesPath))
                {
                    var lines = File.ReadAllLines(RecentFilesPath);
                    _recentFiles.AddRange(lines.Where(File.Exists).Take(MaxRecentFiles));
                }
            }
            catch
            {
                // Ignore errors loading recent files
            }
        }

        private void SaveRecentFiles()
        {
            try
            {
                var directory = Path.GetDirectoryName(RecentFilesPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllLines(RecentFilesPath, _recentFiles);
            }
            catch
            {
                // Ignore errors saving recent files
            }
        }

        private void AddToRecentFiles(string filePath)
        {
            var fullPath = Path.GetFullPath(filePath);
            _recentFiles.Remove(fullPath);
            _recentFiles.Insert(0, fullPath);

            if (_recentFiles.Count > MaxRecentFiles)
            {
                _recentFiles.RemoveRange(MaxRecentFiles, _recentFiles.Count - MaxRecentFiles);
            }

            SaveRecentFiles();
            UpdateRecentFilesMenu();
        }

        private void UpdateRecentFilesMenu()
        {
            recentFilesToolStripMenuItem.DropDownItems.Clear();

            if (_recentFiles.Count == 0)
            {
                var emptyItem = new ToolStripMenuItem("(No recent files)")
                {
                    Enabled = false
                };
                recentFilesToolStripMenuItem.DropDownItems.Add(emptyItem);
                return;
            }

            for (int i = 0; i < _recentFiles.Count; i++)
            {
                var filePath = _recentFiles[i];
                var menuItem = new ToolStripMenuItem($"&{i + 1}. {Path.GetFileName(filePath)}")
                {
                    Tag = filePath,
                    ToolTipText = filePath
                };
                menuItem.Click += RecentFileMenuItem_Click;
                recentFilesToolStripMenuItem.DropDownItems.Add(menuItem);
            }

            recentFilesToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

            var clearItem = new ToolStripMenuItem("&Clear Recent Files");
            clearItem.Click += ClearRecentFiles_Click;
            recentFilesToolStripMenuItem.DropDownItems.Add(clearItem);
        }

        private void RecentFileMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.Tag is string filePath)
            {
                if (File.Exists(filePath))
                {
                    if (!PromptSaveChanges())
                        return;

                    LoadMarkdownFile(filePath);
                }
                else
                {
                    MessageBox.Show($"File not found:\n{filePath}", "File Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _recentFiles.Remove(filePath);
                    SaveRecentFiles();
                    UpdateRecentFilesMenu();
                }
            }
        }

        private void ClearRecentFiles_Click(object? sender, EventArgs e)
        {
            _recentFiles.Clear();
            SaveRecentFiles();
            UpdateRecentFilesMenu();
            toolStripStatusLabel.Text = "Recent files cleared";
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Formatting Toolbar Handlers

        private void WrapSelectedText(string prefix, string suffix)
        {
            var selectionStart = markdownEditor.SelectionStart;
            var selectionLength = markdownEditor.SelectionLength;
            var selectedText = markdownEditor.SelectedText;

            if (selectionLength == 0)
            {
                selectedText = "text";
            }

            var newText = prefix + selectedText + suffix;
            markdownEditor.SelectedText = newText;

            if (selectionLength == 0)
            {
                markdownEditor.SelectionStart = selectionStart + prefix.Length;
                markdownEditor.SelectionLength = selectedText.Length;
            }
            else
            {
                markdownEditor.SelectionStart = selectionStart;
                markdownEditor.SelectionLength = newText.Length;
            }

            markdownEditor.Focus();
        }

        private void InsertAtLineStart(string prefix)
        {
            var selectionStart = markdownEditor.SelectionStart;
            var text = markdownEditor.Text;

            var lineStart = text.LastIndexOf('\n', Math.Max(0, selectionStart - 1)) + 1;

            markdownEditor.SelectionStart = lineStart;
            markdownEditor.SelectionLength = 0;
            markdownEditor.SelectedText = prefix;

            markdownEditor.SelectionStart = selectionStart + prefix.Length;
            markdownEditor.Focus();
        }

        private void InsertText(string textToInsert)
        {
            var selectionStart = markdownEditor.SelectionStart;
            markdownEditor.SelectedText = textToInsert;
            markdownEditor.SelectionStart = selectionStart + textToInsert.Length;
            markdownEditor.Focus();
        }

        private void InsertTextAndSelect(string textToInsert, string textToSelect)
        {
            var selectionStart = markdownEditor.SelectionStart;
            InsertText(textToInsert);
            var selectStart = selectionStart + textToInsert.IndexOf(textToSelect, StringComparison.Ordinal);
            if (selectStart >= selectionStart)
            {
                markdownEditor.SelectionStart = selectStart;
                markdownEditor.SelectionLength = textToSelect.Length;
            }
        }

        private bool TryBeginExport(out string errorMessage)
        {
            if (!_hasOpenDocument)
            {
                errorMessage = "Open or create a document before exporting.";
                return false;
            }

            if (string.IsNullOrEmpty(_currentHtml))
            {
                errorMessage = "Preview is not ready yet. Try again in a moment.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _previewTimer?.Stop();
                _previewTimer?.Dispose();
                _previewTimer = null;

                if (webView?.CoreWebView2 != null)
                {
                    webView.CoreWebView2.NavigationCompleted -= WebView_NavigationCompleted;
                    webView.CoreWebView2.WebMessageReceived -= WebView_WebMessageReceived;
                }

                components?.Dispose();
                _findDialog?.Dispose();
                _imageOpenFileDialog?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void Heading1MenuItem_Click(object sender, EventArgs e) => InsertAtLineStart("# ");
        private void Heading2MenuItem_Click(object sender, EventArgs e) => InsertAtLineStart("## ");
        private void Heading3MenuItem_Click(object sender, EventArgs e) => InsertAtLineStart("### ");
        private void Heading4MenuItem_Click(object sender, EventArgs e) => InsertAtLineStart("#### ");
        private void Heading5MenuItem_Click(object sender, EventArgs e) => InsertAtLineStart("##### ");
        private void Heading6MenuItem_Click(object sender, EventArgs e) => InsertAtLineStart("###### ");

        private void BoldButton_Click(object sender, EventArgs e) => WrapSelectedText("**", "**");
        private void ItalicButton_Click(object sender, EventArgs e) => WrapSelectedText("*", "*");
        private void StrikethroughButton_Click(object sender, EventArgs e) => WrapSelectedText("~~", "~~");

        private void BulletListButton_Click(object sender, EventArgs e) => InsertAtLineStart("- ");
        private void NumberedListButton_Click(object sender, EventArgs e) => InsertAtLineStart("1. ");
        private void TaskListButton_Click(object sender, EventArgs e) => InsertAtLineStart("- [ ] ");

        private void LinkButton_Click(object sender, EventArgs e)
        {
            var selectedText = markdownEditor.SelectedText;
            if (string.IsNullOrEmpty(selectedText))
            {
                InsertTextAndSelect("[link text](https://example.com)", "link text");
            }
            else
            {
                WrapSelectedText("[", "](https://example.com)");
            }
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            var selectedText = markdownEditor.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                WrapSelectedText("![", "](image-url.png)");
                return;
            }

            InsertImageFromFile();
        }

        private void CodeButton_Click(object sender, EventArgs e) => WrapSelectedText("`", "`");

        private void CodeBlockButton_Click(object sender, EventArgs e)
        {
            var selectedText = markdownEditor.SelectedText;
            if (string.IsNullOrEmpty(selectedText))
            {
                InsertTextAndSelect("```\ncode here\n```", "code here");
            }
            else
            {
                WrapSelectedText("```\n", "\n```");
            }
        }

        private void QuoteButton_Click(object sender, EventArgs e) => InsertAtLineStart("> ");

        private void HorizontalRuleButton_Click(object sender, EventArgs e)
        {
            var selectionStart = markdownEditor.SelectionStart;
            var text = markdownEditor.Text;

            var needsNewlineBefore = selectionStart > 0 && text[selectionStart - 1] != '\n';
            var rule = (needsNewlineBefore ? "\n" : "") + "\n---\n\n";

            InsertText(rule);
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            var table = @"| Header 1 | Header 2 | Header 3 |
|----------|----------|----------|
| Cell 1   | Cell 2   | Cell 3   |
| Cell 4   | Cell 5   | Cell 6   |

";
            InsertText(table);
        }

        #endregion
    }
}
