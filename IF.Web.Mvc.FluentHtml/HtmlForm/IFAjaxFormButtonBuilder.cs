using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;
using System;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class IFAjaxFormButtonBuilder: ButtonBuilder
    {
        public IFAjaxFormButtonBuilder(IFButton button):base(button)
        {
            this.HtmlElement.HtmlAttributes.Add("if-ajax-form-submit", "true");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "post");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-update-id", "false");
        }

        public IFAjaxFormButtonBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<IFButton>> configurator)
        {
            UnobtrusiveAjaxBuilder<IFButton> factory = new UnobtrusiveAjaxBuilder<IFButton>(this.HtmlElement);
            configurator(factory);
            return this;
        }

    }
}
