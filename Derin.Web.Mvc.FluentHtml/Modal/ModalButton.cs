using IF.Core.Mvc.PageLayout.SubmitButton;
using System;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Modal
{
    public class ModalButton : Button
    {

        public string DataDismiss { get; set; }

        public string AriaLabel { get; set; }

        public ModalButton(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {

        }

        public override void Build()
        {
            base.Build();

            if (!String.IsNullOrWhiteSpace(this.DataDismiss))
            {
                this.Builder.Attributes.Add("data-dismiss", "modal");
            }

            if (!String.IsNullOrWhiteSpace(this.AriaLabel))
            {
                this.Builder.Attributes.Add("aria-label", "Close");
            }

        }

        public override MvcHtmlString CreateHtml()
        {
            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }
    }
}
