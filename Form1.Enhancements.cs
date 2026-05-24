namespace MarkdownThing
{
    partial class Form1
    {
        private FindDialog? _findDialog;
        private ToolStripMenuItem? findToolStripMenuItem;
        private ToolStripMenuItem? goToLineToolStripMenuItem;
        private ToolStripMenuItem? insertDateTimeToolStripMenuItem;
        private ToolStripMenuItem? copyHtmlToolStripMenuItem;
        private ToolStripMenuItem? reloadToolStripMenuItem;
        private ToolStripMenuItem? openContainingFolderToolStripMenuItem;
        private ToolStripMenuItem? wordWrapToolStripMenuItem;
        private ToolStripMenuItem? previewZoomInMenuItem;
        private ToolStripMenuItem? previewZoomOutMenuItem;
        private ToolStripMenuItem? previewZoomResetMenuItem;
        private ToolStripStatusLabel? toolStripCaretLabel;
        private OpenFileDialog? _imageOpenFileDialog;

        private void InitializeEnhancements()
        {
            InitializeEditEnhancements();
            InitializeFileEnhancements();
            InitializeViewEnhancements();
            InitializeStatusBarEnhancements();
            ApplyPersistedLayoutSettings();
            markdownEditor.SelectionChanged += MarkdownEditor_SelectionChanged;
            webView.MouseWheel += WebView_PreviewMouseWheel;
        }

        private void InitializeEditEnhancements()
        {
            findToolStripMenuItem = new ToolStripMenuItem("&Find...", null, FindToolStripMenuItem_Click)
            {
                ShortcutKeys = Keys.Control | Keys.F,
                ShowShortcutKeys = true
            };
            goToLineToolStripMenuItem = new ToolStripMenuItem("&Go to line...", null, GoToLineToolStripMenuItem_Click)
            {
                ShortcutKeys = Keys.Control | Keys.G,
                ShowShortcutKeys = true
            };
            insertDateTimeToolStripMenuItem = new ToolStripMenuItem("Insert date/time", null, InsertDateTimeMenuItem_Click)
            {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.D,
                ShowShortcutKeys = true
            };
            copyHtmlToolStripMenuItem = new ToolStripMenuItem("Copy &HTML preview", null, CopyHtmlPreviewMenuItem_Click)
            {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.C,
                ShowShortcutKeys = true
            };

            var editInsertIndex = editToolStripMenuItem.DropDownItems.IndexOf(editSeparator3);
            editToolStripMenuItem.DropDownItems.Insert(editInsertIndex++, copyHtmlToolStripMenuItem);
            editToolStripMenuItem.DropDownItems.Insert(editInsertIndex++, new ToolStripSeparator());
            editToolStripMenuItem.DropDownItems.Insert(editInsertIndex++, findToolStripMenuItem);
            editToolStripMenuItem.DropDownItems.Insert(editInsertIndex++, goToLineToolStripMenuItem);
            editToolStripMenuItem.DropDownItems.Insert(editInsertIndex++, insertDateTimeToolStripMenuItem);
            editToolStripMenuItem.DropDownItems.Insert(editInsertIndex, new ToolStripSeparator());

            var dateTimeButton = new ToolStripButton("Date")
            {
                ToolTipText = "Insert date/time (Ctrl+Shift+D)"
            };
            dateTimeButton.Click += InsertDateTimeMenuItem_Click;
            formattingToolStrip.Items.Add(new ToolStripSeparator());
            formattingToolStrip.Items.Add(dateTimeButton);
        }

        private void InitializeFileEnhancements()
        {
            reloadToolStripMenuItem = new ToolStripMenuItem("&Reload from disk", null, ReloadToolStripMenuItem_Click)
            {
                ShortcutKeys = Keys.F5,
                ShowShortcutKeys = true
            };
            openContainingFolderToolStripMenuItem = new ToolStripMenuItem("Open containing &folder", null, OpenContainingFolderMenuItem_Click);

            var insertIndex = fileToolStripMenuItem.DropDownItems.IndexOf(toolStripSeparator3);
            fileToolStripMenuItem.DropDownItems.Insert(insertIndex, openContainingFolderToolStripMenuItem);
            fileToolStripMenuItem.DropDownItems.Insert(insertIndex, reloadToolStripMenuItem);
            fileToolStripMenuItem.DropDownItems.Insert(insertIndex, new ToolStripSeparator());
        }

        private void InitializeViewEnhancements()
        {
            wordWrapToolStripMenuItem = new ToolStripMenuItem("Word &wrap")
            {
                CheckOnClick = true,
                Checked = _settings.WordWrap
            };
            wordWrapToolStripMenuItem.CheckedChanged += (_, _) =>
            {
                _settings.WordWrap = wordWrapToolStripMenuItem!.Checked;
                ApplyWordWrap();
                _settings.Save();
            };

            previewZoomInMenuItem = new ToolStripMenuItem("Preview zoom &in", null, (_, _) => AdjustPreviewZoom(0.1))
            {
                ShortcutKeys = Keys.Control | Keys.Oemplus,
                ShowShortcutKeys = true
            };
            previewZoomOutMenuItem = new ToolStripMenuItem("Preview zoom &out", null, (_, _) => AdjustPreviewZoom(-0.1))
            {
                ShortcutKeys = Keys.Control | Keys.OemMinus,
                ShowShortcutKeys = true
            };
            previewZoomResetMenuItem = new ToolStripMenuItem("Reset preview &zoom", null, (_, _) => SetPreviewZoom(1.0))
            {
                ShortcutKeys = Keys.Control | Keys.D0,
                ShowShortcutKeys = true
            };

            viewToolStripMenuItem!.DropDownItems.Add(new ToolStripSeparator());
            viewToolStripMenuItem.DropDownItems.Add(wordWrapToolStripMenuItem);
            viewToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            viewToolStripMenuItem.DropDownItems.Add(previewZoomInMenuItem);
            viewToolStripMenuItem.DropDownItems.Add(previewZoomOutMenuItem);
            viewToolStripMenuItem.DropDownItems.Add(previewZoomResetMenuItem);

            UpdateEnhancementMenuState();
        }

        private void InitializeStatusBarEnhancements()
        {
            toolStripCaretLabel = new ToolStripStatusLabel
            {
                Text = "Ln 1, Col 1",
                BorderSides = ToolStripStatusLabelBorderSides.Left,
                BorderStyle = Border3DStyle.Etched,
                AutoSize = false,
                Width = 100
            };
            statusStrip.Items.Insert(statusStrip.Items.IndexOf(toolStripWordCountLabel), toolStripCaretLabel);
            UpdateCaretPosition();
        }

        private void ApplyPersistedLayoutSettings()
        {
            ApplyWordWrap();
            SetPreviewZoom(_settings.PreviewZoom, persist: false);

            if (_settings.SplitterDistance > splitContainer.Panel1MinSize &&
                _settings.SplitterDistance < splitContainer.Width - splitContainer.Panel2MinSize)
            {
                splitContainer.SplitterDistance = _settings.SplitterDistance;
            }

            if (_settings.WindowWidth is >= 400 and var width &&
                _settings.WindowHeight is >= 300 and var height)
            {
                StartPosition = FormStartPosition.Manual;
                var x = _settings.WindowX ?? (Screen.PrimaryScreen?.WorkingArea.Width - width) / 2 ?? 100;
                var y = _settings.WindowY ?? (Screen.PrimaryScreen?.WorkingArea.Height - height) / 2 ?? 100;
                Bounds = new Rectangle(x, y, width, height);
            }

            if (_settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;
        }

        private void SaveWindowSettings()
        {
            if (WindowState == FormWindowState.Normal)
            {
                _settings.WindowWidth = Width;
                _settings.WindowHeight = Height;
                _settings.WindowX = Location.X;
                _settings.WindowY = Location.Y;
                _settings.WindowMaximized = false;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                _settings.WindowMaximized = true;
            }

            _settings.SplitterDistance = splitContainer.SplitterDistance;
            _settings.Save();
        }

        private void ApplyWordWrap()
        {
            markdownEditor.WordWrap = _settings.WordWrap;
            markdownEditor.ScrollBars = _settings.WordWrap ? ScrollBars.Vertical : ScrollBars.Both;
            if (wordWrapToolStripMenuItem != null)
                wordWrapToolStripMenuItem.Checked = _settings.WordWrap;
        }

        private void SetPreviewZoom(double zoom, bool persist = true)
        {
            zoom = Math.Clamp(Math.Round(zoom, 2), 0.5, 3.0);
            webView.ZoomFactor = zoom;
            if (persist)
            {
                _settings.PreviewZoom = zoom;
                _settings.Save();
            }
        }

        private void AdjustPreviewZoom(double delta) =>
            SetPreviewZoom(webView.ZoomFactor + delta);

        private void WebView_PreviewMouseWheel(object? sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                AdjustPreviewZoom(e.Delta > 0 ? 0.1 : -0.1);
            }
        }

        private void MarkdownEditor_SelectionChanged(object? sender, EventArgs e) =>
            UpdateCaretPosition();

        private void UpdateCaretPosition()
        {
            if (toolStripCaretLabel == null)
                return;

            var (line, column) = GetCaretLineColumn();
            toolStripCaretLabel.Text = $"Ln {line}, Col {column}";
        }

        private (int Line, int Column) GetCaretLineColumn()
        {
            var index = markdownEditor.SelectionStart;
            var text = markdownEditor.Text;
            if (text.Length == 0)
                return (1, 1);

            var line = 1;
            for (var i = 0; i < index && i < text.Length; i++)
            {
                if (text[i] == '\n')
                    line++;
            }

            var lineStart = text.LastIndexOf('\n', Math.Max(0, index - 1)) + 1;
            var column = index - lineStart + 1;
            return (line, column);
        }

        private void FindToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (!_isEditMode)
                SetEditMode(true);

            _findDialog ??= new FindDialog();
            _findDialog.FindNextRequested -= FindDialog_FindNextRequested;
            _findDialog.FindNextRequested += FindDialog_FindNextRequested;

            if (!_findDialog.Visible)
                _findDialog.Show(this);

            _findDialog.FocusSearchBox();
        }

        private void FindDialog_FindNextRequested(object? sender, EventArgs e)
        {
            if (_findDialog == null)
                return;

            if (!FindNext(_findDialog.SearchText, _findDialog.MatchCase))
            {
                MessageBox.Show("No more matches.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool FindNext(string text, bool matchCase)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            var source = markdownEditor.Text;
            var comparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            var start = markdownEditor.SelectionStart;
            if (markdownEditor.SelectionLength > 0)
                start += markdownEditor.SelectionLength;
            else if (start < source.Length)
                start++;

            var index = start <= source.Length
                ? source.IndexOf(text, start, comparison)
                : -1;
            if (index < 0)
                index = source.IndexOf(text, 0, comparison);

            if (index < 0)
                return false;

            markdownEditor.SelectionStart = index;
            markdownEditor.SelectionLength = text.Length;
            markdownEditor.Focus();
            return true;
        }

        private void GoToLineToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (!_isEditMode)
                SetEditMode(true);

            var maxLine = GetEditorLineCount();
            var currentLine = GetFirstVisibleEditorLine() + 1;
            using var dlg = new GoToLineDialog(maxLine, currentLine);
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            ScrollEditorToLine(dlg.SelectedLine - 1);
            FocusEditorWithoutSelection();
        }

        private void InsertDateTimeMenuItem_Click(object? sender, EventArgs e)
        {
            if (!_isEditMode)
                SetEditMode(true);

            InsertText(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        }

        private void CopyHtmlPreviewMenuItem_Click(object? sender, EventArgs e)
        {
            if (!TryBeginExport(out var exportError))
            {
                MessageBox.Show(exportError, "Copy HTML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FlushPreview();
            try
            {
                Clipboard.SetText(_currentHtml ?? "");
                toolStripStatusLabel.Text = "HTML preview copied to clipboard";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not copy HTML: {ex.Message}", "Copy HTML",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_currentFilePath == null || !File.Exists(_currentFilePath))
            {
                MessageBox.Show("Save the document to a file before reloading from disk.",
                    "Reload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!PromptSaveChanges())
                return;

            LoadMarkdownFile(_currentFilePath);
            toolStripStatusLabel.Text = $"Reloaded: {_currentFilePath}";
        }

        private void OpenContainingFolderMenuItem_Click(object? sender, EventArgs e)
        {
            if (_currentFilePath == null || !File.Exists(_currentFilePath))
            {
                MessageBox.Show("Save the document to a file first.",
                    "Open folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{_currentFilePath}\"",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertImageFromFile()
        {
            if (!_isEditMode)
                SetEditMode(true);

            _imageOpenFileDialog ??= new OpenFileDialog
            {
                Title = "Insert image",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif;*.webp;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.webp;*.bmp|All files (*.*)|*.*"
            };

            if (_currentFilePath != null)
                _imageOpenFileDialog.InitialDirectory = Path.GetDirectoryName(_currentFilePath);

            if (_imageOpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var imagePath = _imageOpenFileDialog.FileName;
            var alt = Path.GetFileNameWithoutExtension(imagePath);
            var linkPath = GetImageMarkdownPath(imagePath);
            InsertTextAndSelect($"![{alt}]({linkPath})", alt);
        }

        private string GetImageMarkdownPath(string imagePath)
        {
            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                var docDir = Path.GetDirectoryName(_currentFilePath);
                if (!string.IsNullOrEmpty(docDir))
                {
                    try
                    {
                        return Path.GetRelativePath(docDir, imagePath).Replace('\\', '/');
                    }
                    catch
                    {
                        // Fall back to file name.
                    }
                }
            }

            return Path.GetFileName(imagePath);
        }

        private void UpdateEnhancementMenuState()
        {
            var hasSavedFile = _currentFilePath != null && File.Exists(_currentFilePath);
            if (reloadToolStripMenuItem != null)
                reloadToolStripMenuItem.Enabled = hasSavedFile;
            if (openContainingFolderToolStripMenuItem != null)
                openContainingFolderToolStripMenuItem.Enabled = hasSavedFile;
            if (copyHtmlToolStripMenuItem != null)
                copyHtmlToolStripMenuItem.Enabled = _hasOpenDocument;
            if (findToolStripMenuItem != null)
                findToolStripMenuItem.Enabled = _isEditMode || _hasOpenDocument;
            if (goToLineToolStripMenuItem != null)
                goToLineToolStripMenuItem.Enabled = _isEditMode || _hasOpenDocument;
            if (insertDateTimeToolStripMenuItem != null)
                insertDateTimeToolStripMenuItem.Enabled = _isEditMode || _hasOpenDocument;
        }
    }
}
