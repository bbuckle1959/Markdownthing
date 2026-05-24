namespace MarkdownThing
{
    partial class Form1
    {
        private const int MaxEditorUndoLevels = 200;

        private readonly List<string> _editorUndoStack = [];
        private string _editorUndoBaseline = "";
        private bool _isApplyingUndoRedo;

        private void InitializeEditorUndo()
        {
            _editorUndoBaseline = markdownEditor.Text;
        }

        private void ResetEditorUndoHistory(string text)
        {
            _editorUndoStack.Clear();
            _editorUndoBaseline = text;
        }

        private void SetEditorText(string text, bool resetUndoHistory = true)
        {
            _isApplyingUndoRedo = true;
            try
            {
                markdownEditor.Text = text;
                if (resetUndoHistory)
                    ResetEditorUndoHistory(text);
            }
            finally
            {
                _isApplyingUndoRedo = false;
            }
        }

        private void RecordEditorChangeForUndo()
        {
            if (_isApplyingUndoRedo)
                return;

            var current = markdownEditor.Text;
            if (current == _editorUndoBaseline)
                return;

            _editorUndoStack.Add(_editorUndoBaseline);
            if (_editorUndoStack.Count > MaxEditorUndoLevels)
                _editorUndoStack.RemoveAt(0);

            _editorUndoBaseline = current;
        }

        private bool CanEditorUndo => _editorUndoStack.Count > 0;

        private void PerformEditorUndo()
        {
            if (!CanEditorUndo)
                return;

            var previous = _editorUndoStack[^1];
            _editorUndoStack.RemoveAt(_editorUndoStack.Count - 1);

            _isApplyingUndoRedo = true;
            try
            {
                markdownEditor.Text = previous;
                _editorUndoBaseline = previous;
            }
            finally
            {
                _isApplyingUndoRedo = false;
            }

            markdownEditor.Focus();
        }
    }
}
