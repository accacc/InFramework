using IF.Web.Mvc.FluentHtml.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class BootstrapAjaxFilterFormBuilder : HtmlFormBuilderBase<BootstrapAjaxFilterForm, BootstrapAjaxFilterFormBuilder>
    {
        public BootstrapAjaxFilterFormBuilder(IHtmlHelper htmlHelper)
        //: base(htmlHelper, ModelId)
        {
            this.HtmlElement = new BootstrapAjaxFilterForm(htmlHelper);
            this.HtmlElement.HtmlAttributes.Add("if-ajax", "true");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "post");
            //this.HtmlElement.RouteValues = new RouteValueDictionary();

        }

        public BootstrapAjaxFilterFormBuilder Title(string ActionDescription)
        {
            this.HtmlElement.Title = ActionDescription;
            return this;
        }

       

        public BootstrapAjaxFilterFormBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<BootstrapAjaxFilterForm>> configurator)
        {
            UnobtrusiveAjaxBuilder<BootstrapAjaxFilterForm> factory = new UnobtrusiveAjaxBuilder<BootstrapAjaxFilterForm>(this.HtmlElement);

            configurator(factory);
            return this;
        }
    }
}
