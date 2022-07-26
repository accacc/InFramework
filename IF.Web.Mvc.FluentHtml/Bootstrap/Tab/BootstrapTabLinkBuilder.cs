using IF.Web.Mvc.FluentHtml.Link;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTabLinkBuilder : ActionLinkBuilderBase<BootstrapTabLink, BootstrapTabLinkBuilder>
    {

        public BootstrapTabLinkBuilder(IHtmlHelper helper, BootstrapTabLink link)
            : base(link)
        {
        }


        public BootstrapTabLinkBuilder Load(string ActionName, string ControllerName, object routeValues)
        {
            this.HtmlElement.ActionName = ActionName;
            this.HtmlElement.ControllerName = ControllerName;

            var route = new RouteValueDictionary(routeValues);

            foreach (var item in route)
            {
                this.HtmlElement.RouteValues.Add(item.Key, item.Value);
            }


            this.HtmlElement.HtmlAttributes.Add("data-bs-toggle", "tabajax");

            return this;
        }


        public BootstrapTabLinkBuilder Load(string ActionName, string ControllerName, RouteValueDictionary routeValues)
        {
            this.HtmlElement.ActionName = ActionName;
            this.HtmlElement.ControllerName = ControllerName;            

            foreach (var item in routeValues)
            {
                this.HtmlElement.RouteValues.Add(item.Key, item.Value);
            }


            this.HtmlElement.HtmlAttributes.Add("data-bs-toggle", "tabajax");

            return this;
        }


        public BootstrapTabLinkBuilder Load(string ActionName, string ControllerName)
        {
            this.HtmlElement.ActionName = ActionName;
            this.HtmlElement.ControllerName = ControllerName;
            this.HtmlElement.HtmlAttributes.Add("data-bs-toggle", "tabajax");
            return this;
        }

        public BootstrapTabLinkBuilder Content(Func<object, object> content)
        {
            this.HtmlElement.Content = content;
            this.HtmlElement.HtmlAttributes.Add("data-bs-toggle", "tab");
            return this;
        }
        public BootstrapTabLinkBuilder Active(bool Active)
        {
            this.HtmlElement.Active = true;
            return this;
        }

    }

}
