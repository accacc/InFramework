using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.Extension
{
    public static class TagBuilderExtension
    {
        public static HtmlString Render(this TagBuilder tag, TagRenderMode mode = TagRenderMode.Normal)
        {
            //tag.TagRenderMode = mode;

            var writer = new System.IO.StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
