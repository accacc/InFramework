
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Base
{





    public interface IHtmlElement
    {
        string CssClass { get; set; }
        string Id { get; set; }
        //string Name { get; set; }
        string InnerText { get; set; }
        RouteValueDictionary HtmlAttributes { get; set; }
        HtmlHelper htmlHelper { get; set; }
        MvcHtmlString Render();

        MvcHtmlString CreateHtml();
        TagBuilder Builder { get; set; }

        string GetTag();

        void Build();
    }
}
 