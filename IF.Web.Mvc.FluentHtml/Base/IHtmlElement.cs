using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;


namespace IF.Web.Mvc.FluentHtml.Base
{
    public interface IHtmlElement
    {
        

        string CssClass { get; set; }
        string Id { get; set; }
        //string Name { get; set; }
        string InnerText { get; set; }
        RouteValueDictionary HtmlAttributes { get; set; }
        IHtmlHelper htmlHelper { get; set; }
        HtmlString Render();

        HtmlString CreateHtml();
        TagBuilder Builder { get; set; }

        string GetTag();

        void Build();
    }
}
