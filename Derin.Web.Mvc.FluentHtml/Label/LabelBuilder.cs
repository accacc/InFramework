using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Label
{
    public  class LabelBuilder : HtmlElementBuilder<IFLabel, LabelBuilder>
    {


        public LabelBuilder(IFLabel label)
        {
            this.HtmlElement = label;
        }

        public LabelBuilder(HtmlHelper htmlHelper,string labelText, string htmlFieldName,ModelMetadata MetaData)
        {
            this.HtmlElement = new IFLabel(htmlHelper, labelText, htmlFieldName,MetaData);
        }

        public new  MvcHtmlString Render()
        {
            return this.HtmlElement.Render();
        }



        public LabelBuilder Text(string Text)
        {
            this.HtmlElement.labelText = Text;
            return this;
        }

        public LabelBuilder For(string For)
        {
            this.HtmlElement.htmlFieldName = For;
            return this;
        }
    }
}
