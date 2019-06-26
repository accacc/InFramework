using IF.Web.Mvc.FluentHtml.Base;

namespace IF.Web.Mvc.FluentHtml.Label
{
    public class LabelFactory
    {
        public HtmlFormElement formElement { get; set; }
        public LabelFactory(HtmlFormElement formElement)
        {
            this.formElement = formElement;
        }

        public LabelBuilder Add()
        {
            
            IFLabel label = new IFLabel(this.formElement.htmlHelper, "",this.formElement.Name,this.formElement.MetaData);
            formElement.Label = label;
            if (!this.formElement.MetaData.IsNullableValueType && this.formElement.MetaData.ModelType.FullName != "System.String")
                label.MetaData.DisplayName += MandatoryFieldHtmlString();
            return new LabelBuilder(label);
        }

        public string MandatoryFieldHtmlString()
        {
            return " <span class='required'>*</span>";
        }
    }
}
