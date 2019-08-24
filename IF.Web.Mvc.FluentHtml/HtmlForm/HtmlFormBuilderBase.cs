using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;
using System;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public abstract class HtmlFormBuilderBase<Element, Builder> : HtmlElementBuilder<Element, Builder>
        where Element : HtmlFormBase
        where Builder : HtmlFormBuilderBase<Element, Builder>
    {

        public Builder Buttons(Action<ButtonFactory> configurator)
        {
            ButtonFactory factory = new ButtonFactory(this.HtmlElement);
            configurator(factory);
            return this as Builder;
        }


        //public Builder Method(string Method)
        //{
        //    this.HtmlElement.Method = Method;
        //    return this as Builder;
        //}



        
        public Builder Content(Func<object, object> value)
        {
            this.HtmlElement.ContentAction = value;
            return this as Builder;
        }


        public Builder DefaultSubmitButton(Action<ButtonBuilder> button)
        {
            IFAjaxFormButtonBuilder builder = new IFAjaxFormButtonBuilder(this.HtmlElement.DefaultSubmitButton);
            button(builder);
            return this as Builder;
        }

        public Builder DefaultCancelButton(Action<ButtonBuilder> button)
        {
            IFAjaxFormButtonBuilder builder = new IFAjaxFormButtonBuilder(this.HtmlElement.DefaultCancelButton);
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
