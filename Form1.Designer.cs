namespace MarkdownThing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            recentFilesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            exportToolStripMenuItem = new ToolStripMenuItem();
            exportToPdfToolStripMenuItem = new ToolStripMenuItem();
            exportToPngToolStripMenuItem = new ToolStripMenuItem();
            exportToHtmlToolStripMenuItem = new ToolStripMenuItem();
            exportToWordToolStripMenuItem = new ToolStripMenuItem();
            exportToTextToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            editSeparator1 = new ToolStripSeparator();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            editSeparator2 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            editSeparator3 = new ToolStripSeparator();
            toggleEditModeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            formattingToolStrip = new ToolStrip();
            headingDropDownButton = new ToolStripDropDownButton();
            heading1MenuItem = new ToolStripMenuItem();
            heading2MenuItem = new ToolStripMenuItem();
            heading3MenuItem = new ToolStripMenuItem();
            heading4MenuItem = new ToolStripMenuItem();
            heading5MenuItem = new ToolStripMenuItem();
            heading6MenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            boldButton = new ToolStripButton();
            italicButton = new ToolStripButton();
            strikethroughButton = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            bulletListButton = new ToolStripButton();
            numberedListButton = new ToolStripButton();
            taskListButton = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            linkButton = new ToolStripButton();
            imageButton = new ToolStripButton();
            toolStripSeparator7 = new ToolStripSeparator();
            codeButton = new ToolStripButton();
            codeBlockButton = new ToolStripButton();
            quoteButton = new ToolStripButton();
            toolStripSeparator8 = new ToolStripSeparator();
            horizontalRuleButton = new ToolStripButton();
            tableButton = new ToolStripButton();
            splitContainer = new SplitContainer();
            editorPanel = new Panel();
            markdownEditor = new ScrollSyncTextBox();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolStripStatusSpring = new ToolStripStatusLabel();
            toolStripWordCountLabel = new ToolStripStatusLabel();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            menuStrip.SuspendLayout();
            formattingToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            editorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1000, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, recentFilesToolStripMenuItem, toolStripSeparator1, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator3, exportToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(195, 22);
            newToolStripMenuItem.Text = "&New document";
            newToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(195, 22);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // recentFilesToolStripMenuItem
            // 
            recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            recentFilesToolStripMenuItem.Size = new Size(195, 22);
            recentFilesToolStripMenuItem.Text = "R&ecent Files";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(192, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(195, 22);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsToolStripMenuItem.Size = new Size(195, 22);
            saveAsToolStripMenuItem.Text = "Save &As...";
            saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(192, 6);
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportToPdfToolStripMenuItem, exportToPngToolStripMenuItem, exportToHtmlToolStripMenuItem, exportToWordToolStripMenuItem, exportToTextToolStripMenuItem });
            exportToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(195, 22);
            exportToolStripMenuItem.Text = "E&xport";
            // 
            // exportToPdfToolStripMenuItem
            // 
            exportToPdfToolStripMenuItem.Name = "exportToPdfToolStripMenuItem";
            exportToPdfToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            exportToPdfToolStripMenuItem.Size = new Size(175, 22);
            exportToPdfToolStripMenuItem.Text = "To &PDF...";
            exportToPdfToolStripMenuItem.Click += ExportToPdfToolStripMenuItem_Click;
            // 
            // exportToPngToolStripMenuItem
            // 
            exportToPngToolStripMenuItem.Name = "exportToPngToolStripMenuItem";
            exportToPngToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
            exportToPngToolStripMenuItem.Size = new Size(175, 22);
            exportToPngToolStripMenuItem.Text = "To &PNG...";
            exportToPngToolStripMenuItem.Click += ExportToPngToolStripMenuItem_Click;
            // 
            // exportToHtmlToolStripMenuItem
            // 
            exportToHtmlToolStripMenuItem.Name = "exportToHtmlToolStripMenuItem";
            exportToHtmlToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            exportToHtmlToolStripMenuItem.Size = new Size(175, 22);
            exportToHtmlToolStripMenuItem.Text = "To &HTML...";
            exportToHtmlToolStripMenuItem.Click += ExportToHtmlToolStripMenuItem_Click;
            // 
            // exportToWordToolStripMenuItem
            // 
            exportToWordToolStripMenuItem.Name = "exportToWordToolStripMenuItem";
            exportToWordToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
            exportToWordToolStripMenuItem.Size = new Size(175, 22);
            exportToWordToolStripMenuItem.Text = "To &Word...";
            exportToWordToolStripMenuItem.Click += ExportToWordToolStripMenuItem_Click;
            // 
            // exportToTextToolStripMenuItem
            // 
            exportToTextToolStripMenuItem.Name = "exportToTextToolStripMenuItem";
            exportToTextToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;
            exportToTextToolStripMenuItem.Size = new Size(175, 22);
            exportToTextToolStripMenuItem.Text = "To &Text...";
            exportToTextToolStripMenuItem.Click += ExportToTextToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(192, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitToolStripMenuItem.Size = new Size(195, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, editSeparator1, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, editSeparator2, selectAllToolStripMenuItem, editSeparator3, toggleEditModeToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(207, 22);
            undoToolStripMenuItem.Text = "&Undo";
            undoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
            // 
            // editSeparator1
            // 
            editSeparator1.Name = "editSeparator1";
            editSeparator1.Size = new Size(204, 6);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cutToolStripMenuItem.Size = new Size(207, 22);
            cutToolStripMenuItem.Text = "Cu&t";
            cutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(207, 22);
            copyToolStripMenuItem.Text = "&Copy";
            copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(207, 22);
            pasteToolStripMenuItem.Text = "&Paste";
            pasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;
            // 
            // editSeparator2
            // 
            editSeparator2.Name = "editSeparator2";
            editSeparator2.Size = new Size(204, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            selectAllToolStripMenuItem.Size = new Size(207, 22);
            selectAllToolStripMenuItem.Text = "Select &All";
            selectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
            // 
            // editSeparator3
            // 
            editSeparator3.Name = "editSeparator3";
            editSeparator3.Size = new Size(204, 6);
            // 
            // toggleEditModeToolStripMenuItem
            // 
            toggleEditModeToolStripMenuItem.Name = "toggleEditModeToolStripMenuItem";
            toggleEditModeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            toggleEditModeToolStripMenuItem.Size = new Size(207, 22);
            toggleEditModeToolStripMenuItem.Text = "&Toggle Edit Mode";
            toggleEditModeToolStripMenuItem.Click += ToggleEditModeToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.ShortcutKeys = Keys.F1;
            aboutToolStripMenuItem.Size = new Size(135, 22);
            aboutToolStripMenuItem.Text = "&About...";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // formattingToolStrip
            // 
            formattingToolStrip.Dock = DockStyle.Top;
            formattingToolStrip.Items.AddRange(new ToolStripItem[] { headingDropDownButton, toolStripSeparator4, boldButton, italicButton, strikethroughButton, toolStripSeparator5, bulletListButton, numberedListButton, taskListButton, toolStripSeparator6, linkButton, imageButton, toolStripSeparator7, codeButton, codeBlockButton, quoteButton, toolStripSeparator8, horizontalRuleButton, tableButton });
            formattingToolStrip.Name = "formattingToolStrip";
            formattingToolStrip.Size = new Size(1000, 25);
            formattingToolStrip.TabIndex = 3;
            formattingToolStrip.Text = "Formatting";
            formattingToolStrip.Visible = false;
            // 
            // headingDropDownButton
            // 
            headingDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            headingDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { heading1MenuItem, heading2MenuItem, heading3MenuItem, heading4MenuItem, heading5MenuItem, heading6MenuItem });
            headingDropDownButton.Name = "headingDropDownButton";
            headingDropDownButton.Size = new Size(65, 22);
            headingDropDownButton.Text = "Heading";
            headingDropDownButton.ToolTipText = "Insert heading";
            // 
            // heading1MenuItem
            // 
            heading1MenuItem.Name = "heading1MenuItem";
            heading1MenuItem.Size = new Size(128, 22);
            heading1MenuItem.Text = "Heading 1";
            heading1MenuItem.Click += Heading1MenuItem_Click;
            // 
            // heading2MenuItem
            // 
            heading2MenuItem.Name = "heading2MenuItem";
            heading2MenuItem.Size = new Size(128, 22);
            heading2MenuItem.Text = "Heading 2";
            heading2MenuItem.Click += Heading2MenuItem_Click;
            // 
            // heading3MenuItem
            // 
            heading3MenuItem.Name = "heading3MenuItem";
            heading3MenuItem.Size = new Size(128, 22);
            heading3MenuItem.Text = "Heading 3";
            heading3MenuItem.Click += Heading3MenuItem_Click;
            // 
            // heading4MenuItem
            // 
            heading4MenuItem.Name = "heading4MenuItem";
            heading4MenuItem.Size = new Size(128, 22);
            heading4MenuItem.Text = "Heading 4";
            heading4MenuItem.Click += Heading4MenuItem_Click;
            // 
            // heading5MenuItem
            // 
            heading5MenuItem.Name = "heading5MenuItem";
            heading5MenuItem.Size = new Size(128, 22);
            heading5MenuItem.Text = "Heading 5";
            heading5MenuItem.Click += Heading5MenuItem_Click;
            // 
            // heading6MenuItem
            // 
            heading6MenuItem.Name = "heading6MenuItem";
            heading6MenuItem.Size = new Size(128, 22);
            heading6MenuItem.Text = "Heading 6";
            heading6MenuItem.Click += Heading6MenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // boldButton
            // 
            boldButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            boldButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            boldButton.Name = "boldButton";
            boldButton.Size = new Size(23, 22);
            boldButton.Text = "B";
            boldButton.ToolTipText = "Bold (Ctrl+B)";
            boldButton.Click += BoldButton_Click;
            // 
            // italicButton
            // 
            italicButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            italicButton.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            italicButton.Name = "italicButton";
            italicButton.Size = new Size(23, 22);
            italicButton.Text = "I";
            italicButton.ToolTipText = "Italic (Ctrl+I)";
            italicButton.Click += ItalicButton_Click;
            // 
            // strikethroughButton
            // 
            strikethroughButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            strikethroughButton.Font = new Font("Segoe UI", 9F, FontStyle.Strikeout);
            strikethroughButton.Name = "strikethroughButton";
            strikethroughButton.Size = new Size(23, 22);
            strikethroughButton.Text = "S";
            strikethroughButton.ToolTipText = "Strikethrough";
            strikethroughButton.Click += StrikethroughButton_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // bulletListButton
            // 
            bulletListButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            bulletListButton.Name = "bulletListButton";
            bulletListButton.Size = new Size(23, 22);
            bulletListButton.Text = "•";
            bulletListButton.ToolTipText = "Bullet list";
            bulletListButton.Click += BulletListButton_Click;
            // 
            // numberedListButton
            // 
            numberedListButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            numberedListButton.Name = "numberedListButton";
            numberedListButton.Size = new Size(23, 22);
            numberedListButton.Text = "1.";
            numberedListButton.ToolTipText = "Numbered list";
            numberedListButton.Click += NumberedListButton_Click;
            // 
            // taskListButton
            // 
            taskListButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            taskListButton.Name = "taskListButton";
            taskListButton.Size = new Size(23, 22);
            taskListButton.Text = "☑";
            taskListButton.ToolTipText = "Task list";
            taskListButton.Click += TaskListButton_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // linkButton
            // 
            linkButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            linkButton.Name = "linkButton";
            linkButton.Size = new Size(33, 22);
            linkButton.Text = "Link";
            linkButton.ToolTipText = "Insert link";
            linkButton.Click += LinkButton_Click;
            // 
            // imageButton
            // 
            imageButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            imageButton.Name = "imageButton";
            imageButton.Size = new Size(44, 22);
            imageButton.Text = "Image";
            imageButton.ToolTipText = "Insert image";
            imageButton.Click += ImageButton_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 25);
            // 
            // codeButton
            // 
            codeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            codeButton.Font = new Font("Consolas", 9F);
            codeButton.Name = "codeButton";
            codeButton.Size = new Size(25, 22);
            codeButton.Text = "<>";
            codeButton.ToolTipText = "Inline code";
            codeButton.Click += CodeButton_Click;
            // 
            // codeBlockButton
            // 
            codeBlockButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            codeBlockButton.Font = new Font("Consolas", 9F);
            codeBlockButton.Name = "codeBlockButton";
            codeBlockButton.Size = new Size(32, 22);
            codeBlockButton.Text = "```";
            codeBlockButton.ToolTipText = "Code block";
            codeBlockButton.Click += CodeBlockButton_Click;
            // 
            // quoteButton
            // 
            quoteButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            quoteButton.Name = "quoteButton";
            quoteButton.Size = new Size(44, 22);
            quoteButton.Text = "Quote";
            quoteButton.ToolTipText = "Blockquote";
            quoteButton.Click += QuoteButton_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 25);
            // 
            // horizontalRuleButton
            // 
            horizontalRuleButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            horizontalRuleButton.Name = "horizontalRuleButton";
            horizontalRuleButton.Size = new Size(27, 22);
            horizontalRuleButton.Text = "HR";
            horizontalRuleButton.ToolTipText = "Horizontal rule";
            horizontalRuleButton.Click += HorizontalRuleButton_Click;
            // 
            // tableButton
            // 
            tableButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tableButton.Name = "tableButton";
            tableButton.Size = new Size(39, 22);
            tableButton.Text = "Table";
            tableButton.ToolTipText = "Insert table";
            tableButton.Click += TableButton_Click;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(editorPanel);
            splitContainer.Panel1Collapsed = true;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(webView);
            splitContainer.Size = new Size(1000, 604);
            splitContainer.SplitterDistance = 450;
            splitContainer.TabIndex = 1;
            // 
            // editorPanel
            // 
            editorPanel.Controls.Add(markdownEditor);
            editorPanel.Dock = DockStyle.Fill;
            editorPanel.Location = new Point(0, 0);
            editorPanel.Name = "editorPanel";
            editorPanel.Size = new Size(450, 100);
            editorPanel.TabIndex = 1;
            // 
            // markdownEditor
            // 
            markdownEditor.AcceptsReturn = true;
            markdownEditor.AcceptsTab = true;
            markdownEditor.Dock = DockStyle.Fill;
            markdownEditor.Font = new Font("Consolas", 11F);
            markdownEditor.Location = new Point(0, 0);
            markdownEditor.Multiline = true;
            markdownEditor.Name = "markdownEditor";
            markdownEditor.ScrollBars = ScrollBars.Both;
            markdownEditor.Size = new Size(450, 100);
            markdownEditor.TabIndex = 0;
            markdownEditor.WordWrap = false;
            markdownEditor.TextChanged += MarkdownEditor_TextChanged;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(1000, 604);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripStatusSpring, toolStripWordCountLabel });
            statusStrip.Location = new Point(0, 628);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1000, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(39, 17);
            toolStripStatusLabel.Text = "Ready";
            // 
            // toolStripStatusSpring
            // 
            toolStripStatusSpring.Name = "toolStripStatusSpring";
            toolStripStatusSpring.Spring = true;
            toolStripStatusSpring.Size = new Size(0, 17);
            // 
            // toolStripWordCountLabel
            // 
            toolStripWordCountLabel.Name = "toolStripWordCountLabel";
            toolStripWordCountLabel.Size = new Size(200, 17);
            toolStripWordCountLabel.TextAlign = ContentAlignment.MiddleRight;
            toolStripWordCountLabel.Text = "";
            // 
            // openFileDialog
            // 
            openFileDialog.DefaultExt = "md";
            openFileDialog.Filter = "Markdown files (*.md)|*.md|All files (*.*)|*.*";
            openFileDialog.Title = "Open Markdown File";
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "md";
            saveFileDialog.Filter = "Markdown files (*.md)|*.md|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Markdown File";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(splitContainer);
            Controls.Add(formattingToolStrip);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MarkdownThing";
            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;
            FormClosing += Form1_FormClosing;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            formattingToolStrip.ResumeLayout(false);
            formattingToolStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            editorPanel.ResumeLayout(false);
            editorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem recentFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem exportToPdfToolStripMenuItem;
        private ToolStripMenuItem exportToPngToolStripMenuItem;
        private ToolStripMenuItem exportToHtmlToolStripMenuItem;
        private ToolStripMenuItem exportToWordToolStripMenuItem;
        private ToolStripMenuItem exportToTextToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripSeparator editSeparator1;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator editSeparator2;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripSeparator editSeparator3;
        private ToolStripMenuItem toggleEditModeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStrip formattingToolStrip;
        private ToolStripDropDownButton headingDropDownButton;
        private ToolStripMenuItem heading1MenuItem;
        private ToolStripMenuItem heading2MenuItem;
        private ToolStripMenuItem heading3MenuItem;
        private ToolStripMenuItem heading4MenuItem;
        private ToolStripMenuItem heading5MenuItem;
        private ToolStripMenuItem heading6MenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton boldButton;
        private ToolStripButton italicButton;
        private ToolStripButton strikethroughButton;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton bulletListButton;
        private ToolStripButton numberedListButton;
        private ToolStripButton taskListButton;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton linkButton;
        private ToolStripButton imageButton;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton codeButton;
        private ToolStripButton codeBlockButton;
        private ToolStripButton quoteButton;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton horizontalRuleButton;
        private ToolStripButton tableButton;
        private SplitContainer splitContainer;
        private Panel editorPanel;
        private ScrollSyncTextBox markdownEditor;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripStatusLabel toolStripStatusSpring;
        private ToolStripStatusLabel toolStripWordCountLabel;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
    }
}
