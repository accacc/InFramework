using IF.Web.Mvc.FluentHtml.Base;
using System;
using IF.Web.Mvc.FluentHtml.Link;
namespace IF.Web.Mvc.FluentHtml.Link
{
    public class AjaxLinkBuilder : ActionLinkBuilderBase<ActionLink, AjaxLinkBuilder>
    {
        public AjaxLinkBuilder(ActionLink link):base(link)
             
        {
            this.HtmlElement.HtmlAttributes.Add("if-ajax", "true");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "get");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-show-dialog", "false");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-refresh-grid", "false");

        }       


        public AjaxLinkBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<ActionLink>> configurator)
        {
            UnobtrusiveAjaxBuilder<ActionLink> factory = new UnobtrusiveAjaxBuilder<ActionLink>(this.HtmlElement);
            configurator(factory);
            return this;
        }
    }
}
