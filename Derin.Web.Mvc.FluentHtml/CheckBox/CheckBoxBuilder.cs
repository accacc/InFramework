using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.CheckBox
{



    public class CheckBoxBuilder : HtmlInputElementBuilder<CheckBox, CheckBoxBuilder>
    {

        public CheckBoxBuilder(HtmlHelper htmlHelper, string Name, string Value, ModelMetadata metaData)
        {
            this.HtmlElement = new CheckBox(htmlHelper, Name, Value, metaData);
        }
    }
}
