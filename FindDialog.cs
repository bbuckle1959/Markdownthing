namespace MarkdownThing
{
    internal sealed class FindDialog : Form
    {
        private readonly TextBox _searchBox = new() { Width = 280, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
        private readonly CheckBox _matchCase = new() { Text = "Match case", AutoSize = true };
        private readonly Button _findNext = new() { Text = "Find next", Width = 90 };
        private readonly Button _close = new() { Text = "Close", Width = 90, DialogResult = DialogResult.Cancel };

        public event EventHandler? FindNextRequested;

        public string SearchText => _searchBox.Text;

        public bool MatchCase => _matchCase.Checked;

        public FindDialog()
        {
            Text = "Find";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(320, 110);
            CancelButton = _close;

            var label = new Label
            {
                Text = "Find what:",
                AutoSize = true,
                Location = new Point(12, 16)
            };

            _searchBox.Location = new Point(12, 36);
            _matchCase.Location = new Point(12, 68);
            _findNext.Location = new Point(128, 66);
            _close.Location = new Point(224, 66);

            _findNext.Click += (_, _) => FindNextRequested?.Invoke(this, EventArgs.Empty);
            _searchBox.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FindNextRequested?.Invoke(this, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            Controls.AddRange([label, _searchBox, _matchCase, _findNext, _close]);
            AcceptButton = _findNext;
        }

        public void FocusSearchBox()
        {
            _searchBox.Focus();
            _searchBox.SelectAll();
        }
    }
}
