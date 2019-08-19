using IF.Core.Mvc.PageLayout.SubmitButton;
using IF.Web.Mvc.FluentHtml.Base;
using Microsoft.AspNetCore.Html;
using System;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public abstract class HtmlFormBuilderBase<Element, Builder> : HtmlRouteableElementBuilder<Element, Builder>
        where Element : HtmlFormBase
        where Builder : HtmlFormBuilderBase<Element, Builder>
    {

        public Builder Buttons(Action<ButtonFactory> configurator)
        {
            ButtonFactory factory = new ButtonFactory(this.HtmlElement);
            configurator(factory);
            return this as Builder;
        }


        public Builder Method(string Method)
        {
            this.HtmlElement.Method = Method;
            return this as Builder;
        }



        public Builder Content(IHtmlContent Content)
        {
            this.HtmlElement.Content = Content;
            return this as Builder;
        }

        public Builder Content(Func<object, object> value)
        {
            this.HtmlElement.ContentAction = value;
            return this as Builder;
        }


        public Builder DefaultSubmitButton(Action<ButtonBuilder> button)
        {
            ButtonBuilder builder = new ButtonBuilder(this.HtmlElement.DefaultSubmitButton);
            button(builder);
            return this as Builder;
        }

        public Builder DefaultCancelButton(Action<ButtonBuilder> button)
        {
            ButtonBuilder builder = new ButtonBuilder(this.HtmlElement.DefaultCancelButton);
            button(builder);
            return this as Builder;
        }


        public Builder DefaultButtonPosition(DefaultButtonPosition position)
        {
            this.HtmlElement.DefaultButtonPosition = position;
            return this as Builder;
        }


        public Builder NavigationButtons(bool IsEnabled)
        {
            this.HtmlElement.NavigationButtons = IsEnabled;
            return this as Builder;
        }



    }
}
