using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.TagHelpers
{
    [HtmlTargetElement("datalist", Attributes ="list-items")]
    public class ListItemsTagHelper: TagHelper
    {
        public SelectList ListItems { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            foreach (var di in ListItems)
            {
                output.Content.AppendHtml($"<option value=\"{ di.Value}\">{ di.Text }</option>");
            }
        }
    }
}
