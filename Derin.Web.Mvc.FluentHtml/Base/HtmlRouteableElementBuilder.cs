using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlRouteableElementBuilder<Element, Builder> : HtmlElementBuilder<Element, Builder>
        where Element : HtmlRouteableElement
        where Builder : HtmlElementBuilder<Element, Builder>
    {
        public Builder ActionName(string ActionName)
        {
            this.HtmlElement.ActionName = ActionName;
            return this as Builder;
        }

        public Builder ControllerName(string ControllerName)
        {
            this.HtmlElement.ControllerName = ControllerName;
            return this as Builder;
        }

        public Builder RouteValues(object RouteValues)
        {
            var routeValues = new RouteValueDictionary(RouteValues);

            foreach (var attribute in routeValues)
            {
                this.HtmlElement.RouteValues.Add(attribute.Key, attribute.Value);
            }

            return this as Builder;
        }

    }
}
 