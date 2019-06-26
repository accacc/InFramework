using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Modal
{
    public class ModalBuilder : HtmlElementBuilder<Modal, ModalBuilder>
    {

        public ModalBuilder(HtmlHelper htmlHelper)
        {
            this.HtmlElement = new Modal(htmlHelper);
        }



        public ModalBuilder Content(string Content)
        {
            var element = new HtmlDivElement(this.HtmlElement.htmlHelper);
            element.Build();
            element.Builder.InnerHtml = Content;

            this.HtmlElement.Childs.Add(element);
            return this;
        }

        public ModalBuilder Title(string Title)
        {
            this.HtmlElement.Title = Title;
            return this;
        }

        public ModalBuilder AcceptChild(IHtmlElement child)
        {
            this.HtmlElement.Childs.Add(child);
            return this;
        }





    }
}
