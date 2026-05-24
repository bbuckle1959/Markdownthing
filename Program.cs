using MDConvertLib;

namespace MarkdownThing
{
    internal static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length > 0 && IsCommandLineExport(args))
                return CommandLineExporter.Run(args);

            ApplicationConfiguration.Initialize();

            string? fileToOpen = null;
            if (args.Length > 0)
            {
                var potentialPath = args[0];
                if (!string.IsNullOrWhiteSpace(potentialPath) && File.Exists(potentialPath))
                    fileToOpen = potentialPath;
            }

            Application.Run(new Form1(fileToOpen));
            return 0;
        }

        private static bool IsCommandLineExport(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg is "-h" or "--help"
                    or "--batch" or "--pdf" or "--png" or "--html" or "--docx" or "--text"
                    or "--theme" or "--dark" or "--paper" or "--format")
                    return true;
            }
            return false;
        }
    }
}
