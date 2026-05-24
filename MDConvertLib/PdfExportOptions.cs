using PuppeteerSharp.Media;

namespace MDConvertLib
{
    public class PdfExportOptions
    {
        public PaperFormat PaperFormat { get; set; } = PaperFormat.A4;
        public string MarginTop { get; set; } = "20mm";
        public string MarginBottom { get; set; } = "20mm";
        public string MarginLeft { get; set; } = "15mm";
        public string MarginRight { get; set; } = "15mm";

        public static PaperFormat ParsePaper(string? name) => name?.ToUpperInvariant() switch
        {
            "LETTER" => PaperFormat.Letter,
            "LEGAL" => PaperFormat.Legal,
            "A3" => PaperFormat.A3,
            "A5" => PaperFormat.A5,
            _ => PaperFormat.A4
        };
    }
}
