using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlRouteableElement : HtmlElement
    {
        public RouteValueDictionary RouteValues { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public HtmlRouteableElement(HtmlHelper htmlHelper, string ActionName, string ControllerName)
            : base(htmlHelper)
        {
            this.ControllerName = ControllerName;
            this.ActionName = ActionName;
            this.RouteValues = new  RouteValueDictionary();
            this.htmlHelper = htmlHelper;
        }

        public HtmlRouteableElement(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }


    }
}
 