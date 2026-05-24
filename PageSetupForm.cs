namespace MarkdownThing
{
    public class PageSetupForm : Form
    {
        private readonly ComboBox _paperCombo;
        private readonly NumericUpDown _top;
        private readonly NumericUpDown _bottom;
        private readonly NumericUpDown _left;
        private readonly NumericUpDown _right;

        public PageSetupForm(AppSettings settings)
        {
            Text = "PDF page setup";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(340, 220);
            AppIcons.ApplyTo(this);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(12)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            layout.Controls.Add(new Label { Text = "Paper size", AutoSize = true }, 0, 0);
            _paperCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            _paperCombo.Items.AddRange(["A4", "Letter", "Legal", "A3", "A5"]);
            _paperCombo.SelectedItem = settings.PdfPaper;
            if (_paperCombo.SelectedIndex < 0) _paperCombo.SelectedIndex = 0;
            layout.Controls.Add(_paperCombo, 1, 0);

            _top = AddMarginRow(layout, "Top margin (mm)", settings.PdfMarginTopMm, 1);
            _bottom = AddMarginRow(layout, "Bottom margin (mm)", settings.PdfMarginBottomMm, 2);
            _left = AddMarginRow(layout, "Left margin (mm)", settings.PdfMarginLeftMm, 3);
            _right = AddMarginRow(layout, "Right margin (mm)", settings.PdfMarginRightMm, 4);

            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Bottom,
                Height = 44,
                Padding = new Padding(8)
            };
            var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Width = 80 };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80 };
            buttonPanel.Controls.Add(ok);
            buttonPanel.Controls.Add(cancel);

            Controls.Add(layout);
            Controls.Add(buttonPanel);
            AcceptButton = ok;
            CancelButton = cancel;
        }

        private static NumericUpDown AddMarginRow(TableLayoutPanel layout, string label, int value, int row)
        {
            layout.Controls.Add(new Label { Text = label, AutoSize = true }, 0, row);
            var spin = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 80,
                Value = Math.Clamp(value, 0, 80),
                Dock = DockStyle.Left,
                Width = 80
            };
            layout.Controls.Add(spin, 1, row);
            return spin;
        }

        public void ApplyTo(AppSettings settings)
        {
            settings.PdfPaper = _paperCombo.SelectedItem?.ToString() ?? "A4";
            settings.PdfMarginTopMm = (int)_top.Value;
            settings.PdfMarginBottomMm = (int)_bottom.Value;
            settings.PdfMarginLeftMm = (int)_left.Value;
            settings.PdfMarginRightMm = (int)_right.Value;
        }
    }
}
