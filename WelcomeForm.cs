namespace MarkdownThing
{
    public class WelcomeForm : Form
    {
        private readonly CheckBox _dontShowAgain = new()
        {
            Text = "Don't show again",
            Location = new Point(16, 256),
            AutoSize = true,
            Checked = true
        };

        public bool OpenSample { get; private set; }

        public bool DontShowAgain => _dontShowAgain.Checked;

        public WelcomeForm()
        {
            Text = "Welcome to MarkdownThing";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(480, 300);
            AppIcons.ApplyTo(this);

            var intro = new Label
            {
                Text = "Open and edit Markdown on Windows, then export to PDF or Word.\r\n\r\n" +
                       "The preview pane is the same layout used for PDF export.\r\n\r\n" +
                       "First PDF export downloads Chromium once (~150 MB) for print-quality output.",
                Location = new Point(16, 16),
                Size = new Size(448, 120),
                AutoSize = false
            };

            var sampleBtn = new Button
            {
                Text = "Open sample document",
                Location = new Point(16, 150),
                Size = new Size(140, 32)
            };
            sampleBtn.Click += (_, _) =>
            {
                OpenSample = true;
                DialogResult = DialogResult.OK;
                Close();
            };

            var openBtn = new Button
            {
                Text = "Open a file...",
                Location = new Point(168, 150),
                Size = new Size(140, 32)
            };
            openBtn.Click += (_, _) =>
            {
                DialogResult = DialogResult.Retry;
                Close();
            };

            var newBtn = new Button
            {
                Text = "Create new document",
                Location = new Point(320, 150),
                Size = new Size(144, 32)
            };
            newBtn.Click += (_, _) =>
            {
                DialogResult = DialogResult.Yes;
                Close();
            };

            var skipBtn = new Button
            {
                Text = "Skip",
                Location = new Point(384, 252),
                Size = new Size(80, 28),
                DialogResult = DialogResult.Cancel
            };

            AcceptButton = newBtn;
            Controls.AddRange([intro, sampleBtn, openBtn, newBtn, skipBtn, _dontShowAgain]);
        }
    }
}
