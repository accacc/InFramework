using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.CheckBox
{
    public class CheckBox : HtmlInputElement
    {
        public CheckBox(HtmlHelper html, string Name, string Value, ModelMetadata metaData)
                : base(html, Name, Value, metaData, InputType.CheckBox)
        {
           
        }



        public override MvcHtmlString CreateHtml()
        {

            if (base.Builder.Attributes["value"].ToLower() == "true")
            {
                base.Builder.Attributes["value"] = "true";
                base.Builder.Attributes["checked"] = "checked";
            }
            else
            {
                base.Builder.Attributes["value"] = "false";
            }



            var html = base.CreateHtml();

            TagBuilder hidden = new TagBuilder("input");
            hidden.Attributes.Add("type", "hidden");
            hidden.Attributes.Add("name", Name);

            if (Value.ToLower() == "true")
            {
                hidden.Attributes.Add("value", "false");
            }
            else
            {
                hidden.Attributes.Add("value", "true");
            }



            html = new MvcHtmlString(html.ToHtmlString() + hidden.ToString(TagRenderMode.SelfClosing));


            return html;

        }
    }
}
