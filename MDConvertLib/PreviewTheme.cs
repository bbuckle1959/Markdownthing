namespace MDConvertLib
{
    public enum PreviewTheme
    {
        Default,
        GitHub,
        Print
    }

    public static class PreviewStyles
    {
        public static string GetCss(PreviewTheme theme, bool dark)
        {
            var baseCss = theme switch
            {
                PreviewTheme.GitHub => GitHubCss,
                PreviewTheme.Print => PrintCss,
                _ => DefaultCss
            };

            return (dark ? DarkShellCss + baseCss + DarkOverridesCss : baseCss) + ScrollSyncCss;
        }

        private const string ScrollSyncCss = """
            h1, h2, h3, h4, h5, h6 { scroll-margin-top: 12px; }
            """;

        private const string DefaultCss = """
            body {
                font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, sans-serif;
                line-height: 1.6;
                max-width: 900px;
                margin: 0 auto;
                padding: 20px;
                color: #333;
            }
            h1, h2, h3, h4, h5, h6 {
                margin-top: 1.5em;
                margin-bottom: 0.5em;
                color: #2c3e50;
            }
            h1 { border-bottom: 2px solid #3498db; padding-bottom: 0.3em; }
            h2 { border-bottom: 1px solid #bdc3c7; padding-bottom: 0.3em; }
            code {
                background-color: #f4f4f4;
                padding: 2px 6px;
                border-radius: 3px;
                font-family: Consolas, Monaco, monospace;
                font-size: 0.9em;
            }
            pre {
                background-color: #f4f4f4;
                padding: 16px;
                border-radius: 6px;
                overflow-x: auto;
            }
            pre code { padding: 0; background: none; }
            blockquote {
                border-left: 4px solid #3498db;
                margin: 1em 0;
                padding: 0.5em 1em;
                background-color: #f9f9f9;
            }
            table { border-collapse: collapse; width: 100%; margin: 1em 0; }
            th, td { border: 1px solid #ddd; padding: 8px 12px; text-align: left; }
            th { background-color: #3498db; color: white; }
            tr:nth-child(even) { background-color: #f9f9f9; }
            a { color: #3498db; }
            img { max-width: 100%; height: auto; }
            hr { border: none; border-top: 1px solid #ddd; margin: 2em 0; }
            ul, ol { padding-left: 2em; }
            li { margin: 0.3em 0; }
            """;

        private const string GitHubCss = """
            body {
                font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Helvetica, Arial, sans-serif;
                font-size: 16px;
                line-height: 1.5;
                max-width: 980px;
                margin: 0 auto;
                padding: 32px;
                color: #24292f;
            }
            h1, h2, h3, h4, h5, h6 {
                margin-top: 24px;
                margin-bottom: 16px;
                font-weight: 600;
                line-height: 1.25;
            }
            h1 { font-size: 2em; border-bottom: 1px solid #d0d7de; padding-bottom: 0.3em; }
            h2 { font-size: 1.5em; border-bottom: 1px solid #d0d7de; padding-bottom: 0.3em; }
            code {
                padding: 0.2em 0.4em;
                margin: 0;
                font-size: 85%;
                background-color: rgba(175, 184, 193, 0.2);
                border-radius: 6px;
                font-family: ui-monospace, SFMono-Regular, Consolas, monospace;
            }
            pre {
                padding: 16px;
                overflow: auto;
                font-size: 85%;
                line-height: 1.45;
                background-color: #f6f8fa;
                border-radius: 6px;
            }
            pre code { background: transparent; padding: 0; }
            blockquote {
                padding: 0 1em;
                color: #57606a;
                border-left: 0.25em solid #d0d7de;
                margin: 0 0 16px;
            }
            table { border-spacing: 0; border-collapse: collapse; width: 100%; margin-bottom: 16px; }
            th, td { padding: 6px 13px; border: 1px solid #d0d7de; }
            th { font-weight: 600; background-color: #f6f8fa; }
            tr:nth-child(2n) { background-color: #f6f8fa; }
            a { color: #0969da; text-decoration: none; }
            a:hover { text-decoration: underline; }
            img { max-width: 100%; }
            hr { height: 0.25em; padding: 0; margin: 24px 0; background-color: #d0d7de; border: 0; }
            ul, ol { padding-left: 2em; }
            """;

        private const string PrintCss = """
            body {
                font-family: Georgia, 'Times New Roman', serif;
                font-size: 12pt;
                line-height: 1.5;
                max-width: 100%;
                margin: 0;
                padding: 0;
                color: #000;
            }
            h1, h2, h3 { page-break-after: avoid; color: #000; }
            h1 { font-size: 18pt; border: none; }
            h2 { font-size: 14pt; border: none; }
            code, pre {
                font-family: 'Courier New', monospace;
                font-size: 10pt;
                background: #f5f5f5;
            }
            pre { padding: 8pt; border: 1px solid #ccc; }
            blockquote { border-left: 2pt solid #666; margin-left: 0; padding-left: 12pt; }
            table { width: 100%; }
            th, td { border: 1px solid #333; padding: 4pt 8pt; }
            th { background: #eee; }
            a { color: #000; text-decoration: underline; }
            img { max-width: 100%; }
            """;

        private const string SyntaxCss = """
            pre code { color: inherit; }
            pre[class*='language-'], code[class*='language-'] {
                font-family: Consolas, Monaco, monospace;
            }
            pre > code { display: block; }
            """;

        private const string DarkShellCss = """
            body { background-color: #1e1e1e; color: #d4d4d4; }
            """;

        private const string DarkOverridesCss = """
            body, p, li, td, dd, dt { color: #d4d4d4; }
            h1, h2, h3, h4, h5, h6 { color: #e8e8e8; }
            h1, h2 { border-color: #444; }
            code { background-color: #2d2d2d; color: #d4d4d4; }
            pre { background-color: #2d2d2d; color: #d4d4d4; }
            pre code { color: #d4d4d4; }
            blockquote { background-color: #252526; border-left-color: #569cd6; color: #c8c8c8; }
            th, td { border-color: #444; color: #d4d4d4; }
            th { background-color: #333; color: #fff; }
            tr:nth-child(even), tr:nth-child(2n) { background-color: #252526; }
            a { color: #569cd6; }
            a:visited { color: #9cdcfe; }
            hr { border-top-color: #444; background-color: #444; border-color: #444; }
            .hljs-comment, .hljs-quote { color: #6a9955; }
            .hljs-keyword { color: #569cd6; }
            .hljs-string { color: #ce9178; }
            .hljs-number, .hljs-literal { color: #b5cea8; }
            """;

        public static string SyntaxHighlightingCss => SyntaxCss;
    }
}
