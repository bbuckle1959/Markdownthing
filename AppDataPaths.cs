namespace MarkdownThing
{
    internal static class AppDataPaths
    {
        public const string AppFolderName = "MarkdownThing";
        private const string LegacyFolderName = "MDConvert";

        private static string AppDataRoot =>
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string AppFolder =>
            Path.Combine(AppDataRoot, AppFolderName);

        public static string SettingsFile =>
            Path.Combine(AppFolder, "settings.json");

        public static string RecentFilesFile =>
            Path.Combine(AppFolder, "recentfiles.txt");

        public static string WebView2Folder =>
            Path.Combine(AppFolder, "WebView2");

        public static void MigrateLegacyData()
        {
            var legacyFolder = Path.Combine(AppDataRoot, LegacyFolderName);
            if (!Directory.Exists(legacyFolder) || Directory.Exists(AppFolder))
                return;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(AppFolder)!);
                Directory.Move(legacyFolder, AppFolder);
            }
            catch
            {
                try
                {
                    Directory.CreateDirectory(AppFolder);
                    foreach (var file in Directory.GetFiles(legacyFolder))
                        File.Copy(file, Path.Combine(AppFolder, Path.GetFileName(file)), overwrite: false);
                }
                catch
                {
                    // non-fatal
                }
            }
        }
    }
}
