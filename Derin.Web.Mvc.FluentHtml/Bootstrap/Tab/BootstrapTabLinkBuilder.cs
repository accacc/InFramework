using IF.Web.Mvc.FluentHtml.Link;
using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTabLinkBuilder : ActionLinkBuilderBase<BootstrapTabLink, BootstrapTabLinkBuilder>
    {
        //public BootstrapTabLinkBuilder(HtmlHelper htmlHelper)
        //    : base(htmlHelper)
        //{

        //}

        public BootstrapTabLinkBuilder(HtmlHelper helper, BootstrapTabLink link)
            : base(link)
        {
            //this.HtmlElement = link;
            this.HtmlElement.HtmlAttributes.Add("aria-expanded", "false");
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


            this.HtmlElement.HtmlAttributes.Add("data-toggle", "tabajax");

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


            this.HtmlElement.HtmlAttributes.Add("data-toggle", "tabajax");

            return this;
        }


        public BootstrapTabLinkBuilder Load(string ActionName, string ControllerName)
        {
            this.HtmlElement.ActionName = ActionName;
            this.HtmlElement.ControllerName = ControllerName;
            this.HtmlElement.HtmlAttributes.Add("data-toggle", "tabajax");
            return this;
        }

        public BootstrapTabLinkBuilder Content(string content)
        {
            this.HtmlElement.Content = content;
            this.HtmlElement.HtmlAttributes.Add("data-toggle", "tab");
            return this;
        }
        public BootstrapTabLinkBuilder Active(bool Active)
        {
            this.HtmlElement.HtmlAttributes["aria-expanded"] = "true";
            return this;
        }

    }

}
