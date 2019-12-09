using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.TagHelpers
{
    public class MarkdownTagHelper : TagHelper
    {
        public string Content { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "div";
            output.Content.SetHtmlContent(Markdown.ToHtml(Content));
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
