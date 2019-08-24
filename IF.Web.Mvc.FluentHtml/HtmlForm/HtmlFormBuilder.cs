using IF.Web.Mvc.FluentHtml.Button;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class HtmlFormBuilder : HtmlFormBuilderBase<IFHtmlForm, HtmlFormBuilder>
    {
        public HtmlFormBuilder(IHtmlHelper htmlHelper)
        {
            this.HtmlElement = new IFHtmlForm(htmlHelper);
        }

        public HtmlFormBuilder CancelButton(Action<IFAjaxFormButtonBuilder> button)
        {
            IFAjaxFormButtonBuilder builder = new IFAjaxFormButtonBuilder(this.HtmlElement.CancelButton);
            button(builder);
            return this;
        }

    }
}
