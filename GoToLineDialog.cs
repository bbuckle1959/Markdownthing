namespace MarkdownThing
{
    internal sealed class GoToLineDialog : Form
    {
        private readonly NumericUpDown _lineNumber = new()
        {
            Minimum = 1,
            Maximum = 1_000_000,
            Width = 120,
            Location = new Point(12, 36)
        };

        public int SelectedLine => (int)_lineNumber.Value;

        public GoToLineDialog(int maxLine, int currentLine)
        {
            Text = "Go to line";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(280, 110);

            var label = new Label
            {
                Text = "Line number:",
                AutoSize = true,
                Location = new Point(12, 16)
            };

            _lineNumber.Maximum = Math.Max(1, maxLine);
            _lineNumber.Value = Math.Clamp(currentLine, 1, _lineNumber.Maximum);

            var ok = new Button
            {
                Text = "Go",
                DialogResult = DialogResult.OK,
                Location = new Point(112, 66),
                Width = 75
            };
            var cancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(192, 66),
                Width = 75
            };

            Controls.AddRange([label, _lineNumber, ok, cancel]);
            AcceptButton = ok;
            CancelButton = cancel;
        }
    }
}
