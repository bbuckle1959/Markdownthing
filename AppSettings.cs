using MDConvertLib;

namespace MarkdownThing
{
    public class AppSettings
    {
        public bool HasSeenWelcome { get; set; }
        public PreviewTheme PreviewTheme { get; set; } = PreviewTheme.Default;
        public bool DarkPreview { get; set; }
        public bool DarkEditor { get; set; }
        public string PdfPaper { get; set; } = "A4";
        public int PdfMarginTopMm { get; set; } = 20;
        public int PdfMarginBottomMm { get; set; } = 20;
        public int PdfMarginLeftMm { get; set; } = 15;
        public int PdfMarginRightMm { get; set; } = 15;

        public int? WindowWidth { get; set; }
        public int? WindowHeight { get; set; }
        public int? WindowX { get; set; }
        public int? WindowY { get; set; }
        public bool WindowMaximized { get; set; }
        public int SplitterDistance { get; set; } = 450;
        public bool WordWrap { get; set; }
        public double PreviewZoom { get; set; } = 1.0;

        public static AppSettings Load()
        {
            AppDataPaths.MigrateLegacyData();

            try
            {
                if (File.Exists(AppDataPaths.SettingsFile))
                {
                    var json = File.ReadAllText(AppDataPaths.SettingsFile);
                    return System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch
            {
                // use defaults
            }

            return new AppSettings();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(AppDataPaths.AppFolder);

                var json = System.Text.Json.JsonSerializer.Serialize(this,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(AppDataPaths.SettingsFile, json);
            }
            catch
            {
                // non-fatal
            }
        }

        public PdfExportOptions ToPdfOptions() => new()
        {
            PaperFormat = PdfExportOptions.ParsePaper(PdfPaper),
            MarginTop = $"{PdfMarginTopMm}mm",
            MarginBottom = $"{PdfMarginBottomMm}mm",
            MarginLeft = $"{PdfMarginLeftMm}mm",
            MarginRight = $"{PdfMarginRightMm}mm"
        };
    }
}
