namespace MarkdownThing
{
    /// <summary>
    /// Multiline TextBox that raises <see cref="Scroll"/> when the user scrolls vertically.
    /// </summary>
    internal class ScrollSyncTextBox : TextBox
    {
        private const int WmVScroll = 0x0115;
        private const int WmMouseWheel = 0x020A;

        public event EventHandler? Scroll;
        public event EventHandler? SelectionChanged;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg is WmVScroll or WmMouseWheel)
                Scroll?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
