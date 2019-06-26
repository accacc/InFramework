using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public class HtmlDivElement : HtmlElement
    {
        public HtmlDivElement(IHtmlHelper htmlHelper) : base(htmlHelper)
        {

        }

        public override string GetTag()
        {
            return "div";
        }

        public override HtmlString CreateHtml()
        {
            return this.Builder.Render();
        }

      
    }
}
