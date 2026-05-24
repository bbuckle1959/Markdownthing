using MDConvertLib;

namespace MarkdownThing
{
    partial class Form1
    {
        private ToolStripMenuItem? viewToolStripMenuItem;
        private ToolStripMenuItem? themeToolStripMenuItem;
        private ToolStripMenuItem? themeDefaultMenuItem;
        private ToolStripMenuItem? themeGitHubMenuItem;
        private ToolStripMenuItem? themePrintMenuItem;
        private ToolStripMenuItem? darkPreviewMenuItem;
        private ToolStripMenuItem? darkEditorMenuItem;
        private ToolStripMenuItem? pageSetupMenuItem;
        private ToolStripMenuItem? batchExportMenuItem;
        private ToolStripMenuItem? openHtmlInBrowserMenuItem;
        private ToolStripMenuItem? checkUpdatesMenuItem;
        private ToolStripMenuItem? reportIssueMenuItem;
        private string? _lastHtmlExportPath;
        private ToolStripStatusLabel? toolStripPreviewHintLabel;

        private void InitializeExtraUi()
        {
            viewToolStripMenuItem = new ToolStripMenuItem("&View");
            themeToolStripMenuItem = new ToolStripMenuItem("Preview &theme");
            themeDefaultMenuItem = new ToolStripMenuItem("Default", null, (_, _) => SetPreviewTheme(PreviewTheme.Default));
            themeGitHubMenuItem = new ToolStripMenuItem("GitHub", null, (_, _) => SetPreviewTheme(PreviewTheme.GitHub));
            themePrintMenuItem = new ToolStripMenuItem("Print", null, (_, _) => SetPreviewTheme(PreviewTheme.Print));
            themeToolStripMenuItem.DropDownItems.AddRange([
                themeDefaultMenuItem, themeGitHubMenuItem, themePrintMenuItem
            ]);

            darkPreviewMenuItem = new ToolStripMenuItem("Dark preview")
            {
                CheckOnClick = true,
                Checked = _settings.DarkPreview
            };
            darkPreviewMenuItem.CheckedChanged += (_, _) =>
            {
                _settings.DarkPreview = darkPreviewMenuItem.Checked;
                _settings.Save();
                ApplyEditorTheme();
                UpdatePreview();
            };

            darkEditorMenuItem = new ToolStripMenuItem("Dark editor")
            {
                CheckOnClick = true,
                Checked = _settings.DarkEditor
            };
            darkEditorMenuItem.CheckedChanged += (_, _) =>
            {
                _settings.DarkEditor = darkEditorMenuItem.Checked;
                _settings.Save();
                ApplyEditorTheme();
            };

            viewToolStripMenuItem.DropDownItems.AddRange([
                themeToolStripMenuItem,
                new ToolStripSeparator(),
                darkPreviewMenuItem,
                darkEditorMenuItem
            ]);

            pageSetupMenuItem = new ToolStripMenuItem("PDF page &setup...", null, PageSetupMenuItem_Click);
            batchExportMenuItem = new ToolStripMenuItem("Batch folder...", null, BatchExportMenuItem_Click);
            openHtmlInBrowserMenuItem = new ToolStripMenuItem("Open last HTML in browser", null, OpenHtmlInBrowser_Click)
            {
                Enabled = false
            };

            exportToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            exportToolStripMenuItem.DropDownItems.Add(pageSetupMenuItem);
            exportToolStripMenuItem.DropDownItems.Add(batchExportMenuItem);
            exportToolStripMenuItem.DropDownItems.Add(openHtmlInBrowserMenuItem);

            checkUpdatesMenuItem = new ToolStripMenuItem("Check for &updates...", null, CheckUpdatesMenuItem_Click);
            reportIssueMenuItem = new ToolStripMenuItem("Report an issue", null, ReportIssueMenuItem_Click);
            helpToolStripMenuItem.DropDownItems.Insert(0, checkUpdatesMenuItem);
            helpToolStripMenuItem.DropDownItems.Insert(1, reportIssueMenuItem);
            helpToolStripMenuItem.DropDownItems.Insert(2, new ToolStripSeparator());

            var insertIndex = menuStrip.Items.IndexOf(helpToolStripMenuItem);
            menuStrip.Items.Insert(insertIndex, viewToolStripMenuItem);

            toolStripPreviewHintLabel = new ToolStripStatusLabel
            {
                Text = "PDF matches preview",
                BorderSides = ToolStripStatusLabelBorderSides.Left,
                BorderStyle = Border3DStyle.Etched
            };
            statusStrip.Items.Insert(1, toolStripPreviewHintLabel);

            exportToPdfToolStripMenuItem.ToolTipText = "Export PDF using the current preview layout";
            exportToPngToolStripMenuItem.ToolTipText = "Export PNG screenshot of the current preview (full page)";
            ApplyEditorTheme();
            SyncThemeMenuChecks();
        }

        private void SetPreviewTheme(PreviewTheme theme)
        {
            _settings.PreviewTheme = theme;
            _settings.Save();
            SyncThemeMenuChecks();
            UpdatePreview();
        }

        private void SyncThemeMenuChecks()
        {
            if (themeDefaultMenuItem == null) return;
            themeDefaultMenuItem.Checked = _settings.PreviewTheme == PreviewTheme.Default;
            themeGitHubMenuItem!.Checked = _settings.PreviewTheme == PreviewTheme.GitHub;
            themePrintMenuItem!.Checked = _settings.PreviewTheme == PreviewTheme.Print;
        }

        private void ApplyEditorTheme()
        {
            if (_settings.DarkEditor)
            {
                var editorBg = Color.FromArgb(30, 30, 30);
                var editorFg = Color.FromArgb(212, 212, 212);
                editorPanel.BackColor = editorBg;
                markdownEditor.BackColor = editorBg;
                markdownEditor.ForeColor = editorFg;
            }
            else
            {
                editorPanel.BackColor = SystemColors.Control;
                markdownEditor.BackColor = SystemColors.Window;
                markdownEditor.ForeColor = SystemColors.WindowText;
            }

            webView.DefaultBackgroundColor = _settings.DarkPreview
                ? Color.FromArgb(30, 30, 30)
                : Color.White;
        }

        private void ShowWelcomeIfNeeded()
        {
            if (_settings.HasSeenWelcome) return;

            using var welcome = new WelcomeForm();
            var result = welcome.ShowDialog(this);
            if (welcome.DontShowAgain)
            {
                _settings.HasSeenWelcome = true;
                _settings.Save();
            }

            if (result == DialogResult.OK && welcome.OpenSample)
                LoadSampleDocument();
            else if (result == DialogResult.Retry)
                OpenToolStripMenuItem_Click(this, EventArgs.Empty);
            else if (result == DialogResult.Yes)
                CreateNewDocument();
        }

        private void LoadSampleDocument()
        {
            _currentFilePath = null;
            _currentMarkdown = MarkdownConverter.GetWelcomeMarkdown();
            _savedMarkdown = _currentMarkdown;
            _isModified = false;
            _hasOpenDocument = true;
            SetEditorText(_currentMarkdown);
            SetEditMode(true);
            UpdatePreview();
            UpdateTitle();
            UpdateMenuState();
            UpdateWordCount();
            toolStripStatusLabel.Text = "Sample document loaded";
        }

        private void PageSetupMenuItem_Click(object? sender, EventArgs e)
        {
            using var dlg = new PageSetupForm(_settings);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                dlg.ApplyTo(_settings);
                _settings.Save();
            }
        }

        private void BatchExportMenuItem_Click(object? sender, EventArgs e)
        {
            using var folderDlg = new FolderBrowserDialog
            {
                Description = "Select a folder of Markdown files to export"
            };
            if (folderDlg.ShowDialog() != DialogResult.OK) return;

            using var formatDlg = new Form
            {
                Text = "Batch export format",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(280, 120),
                MaximizeBox = false,
                MinimizeBox = false
            };
            var combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(16, 16),
                Width = 240
            };
            combo.Items.AddRange(["PDF", "PNG", "HTML", "Word (.docx)", "Plain text"]);
            combo.SelectedIndex = 0;
            var ok = new Button { Text = "Export", Location = new Point(176, 72), DialogResult = DialogResult.OK };
            formatDlg.Controls.AddRange([combo, ok]);
            formatDlg.AcceptButton = ok;
            if (formatDlg.ShowDialog(this) != DialogResult.OK) return;

            var files = Directory.GetFiles(folderDlg.SelectedPath, "*.md", SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                MessageBox.Show("No .md files in that folder.", "Batch export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RunBatchExport(files, combo.SelectedIndex);
        }

        private async void RunBatchExport(string[] files, int formatIndex)
        {
            Cursor = Cursors.WaitCursor;
            exportToolStripMenuItem.Enabled = false;
            var done = 0;
            var pdfOpts = _settings.ToPdfOptions();

            try
            {
                foreach (var file in files)
                {
                    toolStripStatusLabel.Text = $"Batch: {Path.GetFileName(file)} ({done + 1}/{files.Length})";
                    var md = await File.ReadAllTextAsync(file);
                    var basePath = Path.Combine(Path.GetDirectoryName(file)!, Path.GetFileNameWithoutExtension(file));

                    switch (formatIndex)
                    {
                        case 1:
                            var pngDoc = _converter.ConvertToHtmlDocument(md, _settings.PreviewTheme, _settings.DarkPreview);
                            await _converter.ConvertToPngAsync(pngDoc, basePath + ".png", null,
                                msg => toolStripStatusLabel.Text = msg);
                            break;
                        case 2:
                            var html = _converter.ConvertToHtmlDocument(md, _settings.PreviewTheme, _settings.DarkPreview);
                            await File.WriteAllTextAsync(basePath + ".html", html);
                            break;
                        case 3:
                            _converter.ConvertToWordDocument(md, basePath + ".docx");
                            break;
                        case 4:
                            await File.WriteAllTextAsync(basePath + ".txt", _converter.ConvertToPlainText(md));
                            break;
                        default:
                            var doc = _converter.ConvertToHtmlDocument(md, _settings.PreviewTheme, _settings.DarkPreview);
                            await _converter.ConvertToPdfAsync(doc, basePath + ".pdf", pdfOpts,
                                msg => toolStripStatusLabel.Text = msg);
                            break;
                    }
                    done++;
                }

                MessageBox.Show($"Exported {done} file(s).", "Batch export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripStatusLabel.Text = $"Batch export finished ({done} files)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Batch export stopped: {ex.Message}", "Batch export",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                UpdateMenuState();
            }
        }

        private void OpenHtmlInBrowser_Click(object? sender, EventArgs e)
        {
            if (_lastHtmlExportPath == null || !File.Exists(_lastHtmlExportPath))
            {
                MessageBox.Show("Export an HTML file first.", "Open in browser",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = _lastHtmlExportPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open in browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CheckUpdatesMenuItem_Click(object? sender, EventArgs e)
        {
            var previousStatus = toolStripStatusLabel.Text;
            toolStripStatusLabel.Text = "Checking for updates...";
            var outcome = await UpdateChecker.CheckAsync();

            switch (outcome.Result)
            {
                case UpdateCheckResult.UpdateAvailable when !string.IsNullOrEmpty(outcome.ReleaseUrl):
                {
                    var answer = MessageBox.Show(
                        "A newer version is available on GitHub. Open the download page?",
                        "Update available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (answer == DialogResult.Yes)
                        LaunchUrl(outcome.ReleaseUrl);
                    break;
                }
                case UpdateCheckResult.UpToDate:
                    MessageBox.Show("You have the latest release.",
                        "Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    MessageBox.Show("Could not reach GitHub to check for updates.",
                        "Updates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            toolStripStatusLabel.Text = previousStatus;
        }

        private void ReportIssueMenuItem_Click(object? sender, EventArgs e) =>
            LaunchUrl(AppConstants.IssuesUrl);

        private static void LaunchUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open:\n{url}\n\n{ex.Message}",
                    "Open link", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
