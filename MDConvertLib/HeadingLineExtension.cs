using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MDConvertLib
{
    /// <summary>
    /// Adds data-source-line attributes to heading elements for editor/preview scroll sync.
    /// </summary>
    public class HeadingLineExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            if (renderer is not HtmlRenderer htmlRenderer)
                return;

            for (var i = 0; i < htmlRenderer.ObjectRenderers.Count; i++)
            {
                if (htmlRenderer.ObjectRenderers[i] is HeadingRenderer)
                {
                    htmlRenderer.ObjectRenderers[i] = new HeadingLineRenderer();
                    break;
                }
            }
        }

        private sealed class HeadingLineRenderer : HtmlObjectRenderer<HeadingBlock>
        {
            protected override void Write(HtmlRenderer renderer, HeadingBlock obj)
            {
                var level = obj.Level;
                var line = obj.Line;

                if (renderer.EnableHtmlForBlock)
                    renderer.WriteLine($"<h{level} data-source-line=\"{line}\" id=\"heading-line-{line}\">");
                else
                    renderer.WriteLine($"<h{level}>");

                renderer.WriteLeafInline(obj);
                renderer.WriteLine($"</h{level}>");
            }
        }
    }
}
