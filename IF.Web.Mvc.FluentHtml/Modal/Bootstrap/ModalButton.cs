using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Modal.Bootstrap
{
    public class ModalButton : IFButton
    {

        public string DataDismiss { get; set; }

        public string AriaLabel { get; set; }

        public ModalButton(IHtmlHelper htmlHelper)
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

        public override HtmlString CreateHtml()
        {
            return this.Builder.Render();
        }
    }
}
