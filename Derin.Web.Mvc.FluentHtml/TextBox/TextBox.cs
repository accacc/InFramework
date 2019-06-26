using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.TextBox
{
    public class TextBox : HtmlInputElement
    {
        

        public TextBox(HtmlHelper html, string Name,string Value, ModelMetadata metaData)
            : base(html,Name,Value,metaData,InputType.Text)
        {
            
        }



        public override MvcHtmlString CreateHtml()
        {
            return base.CreateHtml();

        }
    }
}
