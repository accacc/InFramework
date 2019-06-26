using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlRouteableElement : HtmlElement
    {
        public RouteValueDictionary RouteValues { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public HtmlRouteableElement(IHtmlHelper htmlHelper, string ActionName, string ControllerName)
            : base(htmlHelper)
        {
            this.ControllerName = ControllerName;
            this.ActionName = ActionName;
            this.RouteValues = new RouteValueDictionary();
            this.htmlHelper = htmlHelper;
        }

        public HtmlRouteableElement(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }


    }
}
