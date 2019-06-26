using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public class HtmlDivElement:HtmlElement
    {
        public HtmlDivElement(HtmlHelper htmlHelper):base(htmlHelper)
        {

        }

        public override string GetTag()
        {
            return "div";
        }

        public override System.Web.Mvc.MvcHtmlString CreateHtml()
        {
            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }
    }
}
