
using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;

namespace IF.Web.Mvc.FluentHtml.Button
{
    public class ButtonBuilderBase<Element, Builder>: HtmlRouteableElementBuilder<Element, Builder>
        where Element : IFButton
        where Builder : ButtonBuilderBase<Element, Builder>
    {

        

        public ButtonBuilderBase(IFButton button)
        {
            this.HtmlElement = button as Element;
        }

       

        

       

        public Builder TemplateName(string TemplateName)
        {
            this.HtmlElement.TemplateName = TemplateName;
            return this as Builder;
        }


        public Builder Text(string Text)
        {
            this.HtmlElement.InnerText = Text;
            return this as Builder;
        }

       

        public Builder Hide()
        {
            this.HtmlElement.Hide = true;
            return this as Builder;
        }




        public Builder RedirectTo(string RedirectTo)
        {
            this.HtmlElement.RedirectTo = RedirectTo;
            return this as Builder;
        }
    }
}
