using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace MDConvertLib
{
    internal static class WordDocumentBuilder
    {
        public static void Write(string markdown, string outputPath, MarkdownPipeline pipeline)
        {
            var document = Markdown.Parse(markdown, pipeline);

            using var wordDoc = WordprocessingDocument.Create(outputPath, WordprocessingDocumentType.Document);
            var mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document();
            var body = mainPart.Document.AppendChild(new Body());

            foreach (var block in document)
            {
                switch (block)
                {
                    case HeadingBlock heading:
                        body.AppendChild(MakeHeadingParagraph(heading));
                        break;
                    case ParagraphBlock para:
                        body.AppendChild(MakeParagraph(para.Inline, false));
                        break;
                    case ListBlock list:
                        foreach (var item in list)
                        {
                            if (item is not ListItemBlock listItem) continue;
                            var bullet = list.IsOrdered ? null : "• ";
                            var order = listItem.Order;
                            var prefix = list.IsOrdered ? $"{(order > 0 ? order : 1)}. " : bullet;
                            foreach (var child in listItem)
                            {
                                if (child is ParagraphBlock p)
                                    body.AppendChild(MakeParagraph(p.Inline, false, prefix));
                            }
                        }
                        break;
                    case QuoteBlock quote:
                        foreach (var qChild in quote)
                        {
                            if (qChild is ParagraphBlock qp)
                                body.AppendChild(MakeParagraph(qp.Inline, true));
                        }
                        break;
                    case CodeBlock code:
                        body.AppendChild(MakeCodeParagraph(code));
                        break;
                    case ThematicBreakBlock:
                        body.AppendChild(new Paragraph(new Run(new Text("―".PadRight(40, '―')))));
                        break;
                }
            }

            if (!body.Elements<Paragraph>().Any())
                body.AppendChild(new Paragraph(new Run(new Text(""))));
        }

        private static Paragraph MakeHeadingParagraph(HeadingBlock heading)
        {
            var size = heading.Level switch
            {
                1 => "48",
                2 => "36",
                3 => "28",
                4 => "24",
                5 => "20",
                _ => "18"
            };
            var p = new Paragraph();
            var run = new Run();
            run.PrependChild(new RunProperties(new Bold(), new FontSize { Val = size }));
            if (heading.Inline != null)
                AppendInlines(run, heading.Inline);
            p.AppendChild(run);
            return p;
        }

        private static Paragraph MakeParagraph(ContainerInline? inline, bool quote, string? prefix = null)
        {
            var p = new Paragraph();
            if (quote)
                p.AppendChild(new ParagraphProperties(new Indentation { Left = "720" }));

            var run = new Run();
            if (!string.IsNullOrEmpty(prefix))
                run.AppendChild(new Text(prefix));

            if (inline != null)
                AppendInlines(run, inline);

            if (!run.Elements<Text>().Any() && !run.Elements<Run>().Any())
                run.AppendChild(new Text(""));

            p.AppendChild(run);
            return p;
        }

        private static Paragraph MakeCodeParagraph(CodeBlock code)
        {
            var p = new Paragraph();
            var run = new Run();
            run.PrependChild(new RunProperties(
                new RunFonts { Ascii = "Consolas", HighAnsi = "Consolas" },
                new Shading { Fill = "F4F4F4" }));
            run.AppendChild(new Text(code.Lines.ToString().TrimEnd('\r', '\n')));
            p.AppendChild(run);
            return p;
        }

        private static void AppendInlines(Run run, ContainerInline container)
        {
            foreach (var inline in container)
            {
                switch (inline)
                {
                    case LiteralInline lit:
                        run.AppendChild(new Text(lit.Content.ToString()) { Space = SpaceProcessingModeValues.Preserve });
                        break;
                    case EmphasisInline emph:
                        var props = emph.DelimiterCount >= 2
                            ? new RunProperties(new Bold())
                            : new RunProperties(new Italic());
                        var inner = new Run();
                        inner.PrependChild(props);
                        AppendInlines(inner, emph);
                        run.AppendChild(inner);
                        break;
                    case CodeInline code:
                        var codeRun = new Run(new Text(code.Content.ToString()));
                        codeRun.PrependChild(new RunProperties(
                            new RunFonts { Ascii = "Consolas" },
                            new Shading { Fill = "EEEEEE" }));
                        run.AppendChild(codeRun);
                        break;
                    case LineBreakInline:
                        run.AppendChild(new Break());
                        break;
                    case LinkInline link:
                    {
                        var linkRun = new Run();
                        linkRun.PrependChild(new RunProperties(
                            new DocumentFormat.OpenXml.Wordprocessing.Color { Val = "0563C1" },
                            new Underline { Val = UnderlineValues.Single }));
                        if (link.FirstChild != null)
                            AppendInlines(linkRun, link);
                        else
                            linkRun.AppendChild(new Text(link.Url ?? "") { Space = SpaceProcessingModeValues.Preserve });
                        run.AppendChild(linkRun);
                        break;
                    }
                    case ContainerInline nested:
                        AppendInlines(run, nested);
                        break;
                }
            }
        }
    }
}
