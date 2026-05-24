using System.Text.RegularExpressions;
using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace MDConvertLib
{
    public class MarkdownConverter
    {
        private readonly MarkdownPipeline _pipeline;

        public MarkdownConverter()
        {
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Use<HeadingLineExtension>()
                .Build();
        }

        public string ConvertToHtmlBody(string markdown) =>
            Markdown.ToHtml(markdown, _pipeline);

        public string ConvertToHtmlDocument(string markdown, PreviewTheme theme = PreviewTheme.Default, bool dark = false)
        {
            var htmlBody = ConvertToHtmlBody(markdown);
            return WrapInHtmlDocument(htmlBody, theme, dark);
        }

        public string ConvertToPlainText(string markdown)
        {
            var document = Markdown.Parse(markdown, _pipeline);
            using var writer = new StringWriter();
            var renderer = new HtmlRenderer(writer)
            {
                EnableHtmlForBlock = false,
                EnableHtmlForInline = false,
                EnableHtmlEscape = false
            };
            renderer.Render(document);
            writer.Flush();

            var text = writer.ToString();
            text = Regex.Replace(text, @"<[^>]+>", "");
            text = System.Net.WebUtility.HtmlDecode(text);
            return text;
        }

        public void ConvertToWordDocument(string markdown, string outputPath) =>
            WordDocumentBuilder.Write(markdown, outputPath, _pipeline);

        public Task ConvertToPdfAsync(
            string htmlContent,
            string outputPath,
            PdfExportOptions? options = null,
            Action<string>? status = null) =>
            RenderHtmlAsync(htmlContent, status, async page =>
            {
                options ??= new PdfExportOptions();
                await page.PdfAsync(outputPath, new PdfOptions
                {
                    Format = options.PaperFormat,
                    PrintBackground = true,
                    MarginOptions = new MarginOptions
                    {
                        Top = options.MarginTop,
                        Bottom = options.MarginBottom,
                        Left = options.MarginLeft,
                        Right = options.MarginRight
                    }
                });
            });

        public Task ConvertToPngAsync(
            string htmlContent,
            string outputPath,
            PngExportOptions? options = null,
            Action<string>? status = null)
        {
            options ??= new PngExportOptions();
            var viewport = new ViewPortOptions
            {
                Width = options.ViewportWidth,
                Height = 800,
                DeviceScaleFactor = options.DeviceScaleFactor
            };

            return RenderHtmlAsync(htmlContent, status, page => page.ScreenshotAsync(outputPath, new ScreenshotOptions
            {
                FullPage = true,
                Type = ScreenshotType.Png
            }), viewport);
        }

        private static async Task RenderHtmlAsync(
            string htmlContent,
            Action<string>? status,
            Func<IPage, Task> render,
            ViewPortOptions? viewport = null)
        {
            await EnsureChromiumAsync(status);

            status?.Invoke("Rendering...");

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            if (viewport != null)
                await page.SetViewportAsync(viewport);
            await page.SetJavaScriptEnabledAsync(false);
            await page.SetContentAsync(htmlContent, new NavigationOptions
            {
                Timeout = 60_000,
                WaitUntil = [WaitUntilNavigation.Load]
            });
            await render(page);
        }

        private static async Task EnsureChromiumAsync(Action<string>? status)
        {
            var browserFetcher = new BrowserFetcher();
            if (!browserFetcher.GetInstalledBrowsers().Any())
            {
                status?.Invoke("Downloading Chromium for export (one time, ~150 MB)...");
                await browserFetcher.DownloadAsync();
            }
        }

        public static string WrapInHtmlDocument(
            string htmlBody,
            PreviewTheme theme = PreviewTheme.Default,
            bool dark = false)
        {
            var css = PreviewStyles.GetCss(theme, dark) + PreviewStyles.SyntaxHighlightingCss;
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <style>
{css}
    </style>
</head>
<body>
{htmlBody}
</body>
</html>";
        }

        public static string GetWelcomeMarkdown() => """
            # Welcome to MarkdownThing

            This is a sample document. Edit it, or open your own `.md` file.

            ## What you can do here

            - Type on the left and watch the preview update
            - Export to **PDF**, **PNG**, HTML, Word, or plain text
            - Use **File → Export → Batch folder** for many files at once

            ## A bit of formatting

            | Column | Value |
            |--------|-------|
            | Offline | Yes |
            | Account  | None required |

            ```csharp
            // Code blocks get syntax highlighting in the preview
            var message = "Hello";
            Console.WriteLine(message);
            ```

            > PDF and PNG export use the same HTML as this preview — what you see is what you get.

            Try **File → Export → To PDF** or **To PNG** when you are ready.
            """;
    }
}
