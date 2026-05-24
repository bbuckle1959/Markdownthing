namespace MarkdownThing
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            logoPictureBox = new PictureBox();
            labelProductName = new Label();
            labelVersion = new Label();
            labelCopyright = new Label();
            linkLabelGitHub = new LinkLabel();
            textBoxDescription = new TextBox();
            okButton = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(linkLabelGitHub, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Controls.Add(okButton, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(10, 10);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(526, 405);
            tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.BackColor = Color.FromArgb(52, 152, 219);
            logoPictureBox.Dock = DockStyle.Fill;
            logoPictureBox.Location = new Point(3, 3);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new Size(144, 399);
            logoPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            logoPictureBox.TabIndex = 0;
            logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            labelProductName.AutoSize = true;
            labelProductName.Dock = DockStyle.Fill;
            labelProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelProductName.Location = new Point(156, 0);
            labelProductName.Margin = new Padding(6, 0, 3, 0);
            labelProductName.MaximumSize = new Size(0, 28);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(367, 28);
            labelProductName.TabIndex = 1;
            labelProductName.Text = "MarkdownThing";
            labelProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Dock = DockStyle.Fill;
            labelVersion.Location = new Point(156, 28);
            labelVersion.Margin = new Padding(6, 0, 3, 0);
            labelVersion.MaximumSize = new Size(0, 28);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(367, 28);
            labelVersion.TabIndex = 2;
            labelVersion.Text = "Version";
            labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.AutoSize = true;
            labelCopyright.Dock = DockStyle.Fill;
            labelCopyright.Location = new Point(156, 56);
            labelCopyright.Margin = new Padding(6, 0, 3, 0);
            labelCopyright.MaximumSize = new Size(0, 28);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(367, 28);
            labelCopyright.TabIndex = 3;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // linkLabelGitHub
            // 
            linkLabelGitHub.AutoSize = true;
            linkLabelGitHub.Dock = DockStyle.Fill;
            linkLabelGitHub.Location = new Point(156, 84);
            linkLabelGitHub.Margin = new Padding(6, 0, 3, 0);
            linkLabelGitHub.Name = "linkLabelGitHub";
            linkLabelGitHub.Size = new Size(367, 24);
            linkLabelGitHub.TabIndex = 4;
            linkLabelGitHub.TabStop = true;
            linkLabelGitHub.Text = "https://github.com/bbuckle1959/MarkdownThing";
            linkLabelGitHub.TextAlign = ContentAlignment.MiddleLeft;
            linkLabelGitHub.LinkClicked += LinkLabelGitHub_LinkClicked;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Dock = DockStyle.Fill;
            textBoxDescription.Font = new Font("Consolas", 9F);
            textBoxDescription.Location = new Point(156, 111);
            textBoxDescription.Margin = new Padding(6, 3, 3, 3);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.ScrollBars = ScrollBars.Vertical;
            textBoxDescription.Size = new Size(367, 275);
            textBoxDescription.TabIndex = 5;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "Third-party notices will be displayed here.";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(448, 388);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 29);
            okButton.TabIndex = 6;
            okButton.Text = "&OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // AboutBox
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 425);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About MarkdownThing";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private PictureBox logoPictureBox;
        private Label labelProductName;
        private Label labelVersion;
        private Label labelCopyright;
        private LinkLabel linkLabelGitHub;
        private TextBox textBoxDescription;
        private Button okButton;
    }
}
