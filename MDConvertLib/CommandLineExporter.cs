namespace MDConvertLib
{
    public static class CommandLineExporter
    {
        private static readonly HashSet<string> BatchFormats = new(StringComparer.OrdinalIgnoreCase)
        {
            "pdf", "png", "html", "docx", "txt"
        };

        public static int Run(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            var converter = new MarkdownConverter();
            var theme = PreviewTheme.Default;
            var dark = false;
            var paper = "A4";
            string? input = null;
            string? pdfOut = null;
            string? pngOut = null;
            string? htmlOut = null;
            string? docxOut = null;
            string? txtOut = null;
            string? batchDir = null;
            string batchFormat = "pdf";

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                switch (arg)
                {
                    case "-h":
                    case "--help":
                        PrintUsage();
                        return 0;
                    case "--pdf" when i + 1 < args.Length:
                        pdfOut = args[++i];
                        break;
                    case "--png" when i + 1 < args.Length:
                        pngOut = args[++i];
                        break;
                    case "--html" when i + 1 < args.Length:
                        htmlOut = args[++i];
                        break;
                    case "--docx" when i + 1 < args.Length:
                        docxOut = args[++i];
                        break;
                    case "--text" when i + 1 < args.Length:
                        txtOut = args[++i];
                        break;
                    case "--theme" when i + 1 < args.Length:
                    {
                        var themeArg = args[++i];
                        if (!Enum.TryParse(themeArg, true, out theme))
                        {
                            Console.Error.WriteLine($"Unknown theme: {themeArg}. Use Default, GitHub, or Print.");
                            return 1;
                        }
                        break;
                    }
                    case "--dark":
                        dark = true;
                        break;
                    case "--paper" when i + 1 < args.Length:
                        paper = args[++i];
                        break;
                    case "--batch" when i + 1 < args.Length:
                        batchDir = args[++i];
                        break;
                    case "--format" when i + 1 < args.Length:
                    {
                        batchFormat = args[++i].ToLowerInvariant();
                        if (!BatchFormats.Contains(batchFormat))
                        {
                            Console.Error.WriteLine($"Unknown format: {batchFormat}. Use pdf, png, html, docx, or txt.");
                            return 1;
                        }
                        break;
                    }
                    default:
                        if (!arg.StartsWith('-') && input == null)
                            input = arg;
                        else if (arg.StartsWith('-'))
                        {
                            Console.Error.WriteLine($"Unknown option: {arg}");
                            PrintUsage();
                            return 1;
                        }
                        break;
                }
            }

            if (batchDir != null)
                return RunBatch(converter, batchDir, batchFormat, theme, dark, paper);

            if (input == null || !File.Exists(input))
            {
                Console.Error.WriteLine("Input file not found.");
                PrintUsage();
                return 1;
            }

            if (pdfOut == null && pngOut == null && htmlOut == null && docxOut == null && txtOut == null)
            {
                var baseName = Path.GetFileNameWithoutExtension(input);
                var dir = Path.GetDirectoryName(Path.GetFullPath(input)) ?? ".";
                pdfOut = Path.Combine(dir, baseName + ".pdf");
            }

            return ExportOne(converter, input, theme, dark, paper, pdfOut, pngOut, htmlOut, docxOut, txtOut);
        }

        private static int RunBatch(
            MarkdownConverter converter,
            string folder,
            string format,
            PreviewTheme theme,
            bool dark,
            string paper)
        {
            if (!Directory.Exists(folder))
            {
                Console.Error.WriteLine($"Folder not found: {folder}");
                return 1;
            }

            var files = Directory.GetFiles(folder, "*.md", SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                Console.Error.WriteLine("No .md files found.");
                return 1;
            }

            var ok = 0;
            foreach (var file in files)
            {
                var baseName = Path.GetFileNameWithoutExtension(file);
                var dir = Path.GetDirectoryName(file)!;
                try
                {
                    var code = format switch
                    {
                        "png" => ExportOne(converter, file, theme, dark, paper, null, Path.Combine(dir, baseName + ".png"), null, null, null),
                        "html" => ExportOne(converter, file, theme, dark, paper, null, null, Path.Combine(dir, baseName + ".html"), null, null),
                        "docx" => ExportOne(converter, file, theme, dark, paper, null, null, null, Path.Combine(dir, baseName + ".docx"), null),
                        "txt" => ExportOne(converter, file, theme, dark, paper, null, null, null, null, Path.Combine(dir, baseName + ".txt")),
                        _ => ExportOne(converter, file, theme, dark, paper, Path.Combine(dir, baseName + ".pdf"), null, null, null, null)
                    };

                    if (code == 0)
                    {
                        Console.WriteLine(file);
                        ok++;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"{file}: {ex.Message}");
                }
            }

            Console.WriteLine($"Done. {ok}/{files.Length} files.");
            return ok == files.Length ? 0 : 2;
        }

        private static int ExportOne(
            MarkdownConverter converter,
            string input,
            PreviewTheme theme,
            bool dark,
            string paper,
            string? pdfOut,
            string? pngOut,
            string? htmlOut,
            string? docxOut,
            string? txtOut)
        {
            try
            {
                var md = File.ReadAllText(input);
                var html = converter.ConvertToHtmlDocument(md, theme, dark);

                if (htmlOut != null)
                {
                    File.WriteAllText(htmlOut, html);
                    Console.WriteLine($"HTML: {htmlOut}");
                }

                if (docxOut != null)
                {
                    converter.ConvertToWordDocument(md, docxOut);
                    Console.WriteLine($"Word: {docxOut}");
                }

                if (txtOut != null)
                {
                    File.WriteAllText(txtOut, converter.ConvertToPlainText(md));
                    Console.WriteLine($"Text: {txtOut}");
                }

                if (pdfOut != null)
                {
                    Console.WriteLine("Export may download Chromium on first run...");
                    var opts = new PdfExportOptions { PaperFormat = PdfExportOptions.ParsePaper(paper) };
                    converter.ConvertToPdfAsync(html, pdfOut, opts,
                        msg => Console.WriteLine(msg)).GetAwaiter().GetResult();
                    Console.WriteLine($"PDF: {pdfOut}");
                }

                if (pngOut != null)
                {
                    Console.WriteLine("Export may download Chromium on first run...");
                    converter.ConvertToPngAsync(html, pngOut, null,
                        msg => Console.WriteLine(msg)).GetAwaiter().GetResult();
                    Console.WriteLine($"PNG: {pngOut}");
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{input}: {ex.Message}");
                return 1;
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("""
                MarkdownThing (command line)

                MarkdownThing.exe notes.md --pdf report.pdf
                MarkdownThing.exe notes.md --png preview.png --html out.html
                MarkdownThing.exe --batch C:\Docs --format png
                MarkdownThing.exe notes.md --pdf out.pdf --theme GitHub --dark --paper Letter

                Options:
                  --pdf <file>    Export PDF (default if no output flags)
                  --png <file>    Export PNG screenshot of preview (full page)
                  --html <file>   Export HTML
                  --docx <file>   Export Word
                  --text <file>   Export plain text
                  --batch <dir>   Convert all .md under folder
                  --format pdf|png|html|docx|txt   With --batch (default pdf)
                  --theme Default|GitHub|Print
                  --dark          Dark preview styling for HTML/PDF/PNG
                  --paper A4|Letter|Legal|A3|A5   PDF only
                  -h, --help
                """);
        }
    }
}
