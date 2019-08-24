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


        public Builder SubmitButton(Action<IFAjaxFormButtonBuilder> button)
        {
            IFAjaxFormButtonBuilder builder = new IFAjaxFormButtonBuilder(this.HtmlElement.SubmitButton);
            button(builder);
            return this as Builder;
        }

       

        public Builder DefaultButtonPosition(ButtonPosition position)
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
