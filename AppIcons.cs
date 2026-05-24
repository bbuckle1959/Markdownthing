namespace MarkdownThing
{
    internal static class AppIcons
    {
        public const string FileName = "milcot.ico";

        public static string IconPath => Path.Combine(AppContext.BaseDirectory, FileName);

        public static Icon? Load()
        {
            try
            {
                if (File.Exists(IconPath))
                    return new Icon(IconPath);
            }
            catch
            {
                // Fall back to no custom icon.
            }

            return null;
        }

        public static Bitmap? LoadBitmap()
        {
            using var icon = Load();
            return icon?.ToBitmap();
        }

        public static void ApplyTo(Form form)
        {
            var icon = Load();
            if (icon == null)
                return;

            var previous = form.Icon;
            form.Icon = icon;
            if (!ReferenceEquals(previous, icon))
                previous?.Dispose();
        }
    }
}
