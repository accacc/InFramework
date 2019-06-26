using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.TextBox
{
    public class TextBoxBuilder : HtmlInputElementBuilder<TextBox, TextBoxBuilder>
    {

        public TextBoxBuilder(HtmlHelper htmlHelper,string Name,string Value,ModelMetadata metaData)
        {
            this.HtmlElement = new TextBox(htmlHelper, Name,Value, metaData);
        }

    
    }
}
