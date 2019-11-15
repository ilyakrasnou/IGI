using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FreeRock.TagHelpers
{
    [HtmlTargetElement("cover-image", TagStructure = TagStructure.WithoutEndTag)]
    public class CoverTagHelper: TagHelper
    {
        public string Source { get; set; }
        public string AltName { get; set; }

        public override void Process (TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "img";
            output.Attributes.SetAttribute("src", Source);
            output.Attributes.SetAttribute("alt", AltName);
            output.Attributes.SetAttribute("height", 128);
            output.Attributes.SetAttribute("width", 128);
            output.Attributes.SetAttribute("oneerror", "this.onerror=null;this.src='/covers/default.jpg';");
        }

        /*public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", Source);
            output.Attributes.SetAttribute("alt", AltName);
            output.Attributes.SetAttribute("height", 128);
            output.Attributes.SetAttribute("width", 128);
            output.Attributes.SetAttribute("oneerror", "this.onerror=null;this.src='/covers/default.jpg';");
            return base.ProcessAsync(context, output);
        }*/
    }
}
