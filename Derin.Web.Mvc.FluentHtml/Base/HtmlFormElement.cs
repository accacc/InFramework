using IF.Web.Mvc.FluentHtml.Bootstrap;
using IF.Web.Mvc.FluentHtml.Label;
using IF.Web.Mvc.FluentHtml.Validation;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlFormElement:HtmlElement
    {
        public IFLabel Label { get; set; }

        public ValidationMessage ValidationMessage { get; set; }

        public bool IncludeValidationMessage { get; set; }

        public string Name { get; set; }

        


        public HtmlFormElement(HtmlHelper html, string Name,  ModelMetadata metaData)
            : base(html)
        {
            this.MetaData = metaData;            
            this.Name = Name;

        }


        public MvcHtmlString RenderWithFormGroup()
        {
            BootstrapFormGroup group = new BootstrapFormGroup(this.htmlHelper);

            if (this.Label != null)
            {
                group.AcceptChild(this.Label);
            }

            group.AcceptChild(this);

            if (this.IncludeValidationMessage && this.ValidationMessage != null)
            {
                group.AcceptChild(this.ValidationMessage);
            }

            return group.Render();
        }

        
    }
}
