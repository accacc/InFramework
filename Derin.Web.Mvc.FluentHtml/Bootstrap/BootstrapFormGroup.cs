using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Label;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Bootstrap
{
    public class BootstrapFormGroup : HtmlContainerElement
    {
        public BootstrapFormGroup(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public HtmlInputElement HtmlInput { get; set; }
        public IFLabel Label { get; set; }
        public override void Build()
        {
            base.Build();
            Builder.AddCssClass("form-group");
        }
        public override MvcHtmlString CreateHtml()
        {
            return base.CreateHtml();
        }

        public override string GetTag()
        {
            return "div";
        }

        
    }
}
