using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Html;

namespace IF.Web.Mvc.FluentHtml.Button
{
    public class IFButton : HtmlElement
    {
        public string Value { get; set; }
        public string ActionName { get; set; }
        public string RedirectTo { get; set; }
        public string UpdatedTargetId { get; set; }
        public HtmlFormBase Form { get; set; }
        public string TemplateName { get; set; }
        public string Type { get; set; }

        public string IconClassName { get; set; }

        public bool Hide { get; set; }

        public IFButton(IHtmlHelper htmlHelper, HtmlFormBase form)
            : base(htmlHelper)
        {
            this.Form = form;
            this.Hide = false;
        }

        public IFButton(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {

        }

        public override HtmlString CreateHtml()
        {

            return this.Builder.Render();
        }

        public override void Build()
        {
            base.Build();

            foreach (var att in this.HtmlAttributes)
            {
                this.Builder.Attributes.Add(att.Key, att.Value.ToString());
            }

            this.Builder.Attributes.Add("value", this.Value);


            if (!String.IsNullOrWhiteSpace(this.Type))
            {
                this.Builder.Attributes.Add("type", this.Type);
            }
            else
            {
                this.Builder.Attributes.Add("type", "submit");
            }

            if (!String.IsNullOrWhiteSpace(this.ActionName))
            {
                this.Builder.Attributes.Add("data-action", this.ActionName);
            }

            if (String.IsNullOrWhiteSpace(this.CssClass))
            {
                this.CssClass = "btn";
            }

            if (!String.IsNullOrWhiteSpace(this.RedirectTo))
            {
                this.Builder.Attributes.Add("redirect-url", this.RedirectTo);
            }

            if (!String.IsNullOrWhiteSpace(this.UpdatedTargetId))
            {
                this.Builder.Attributes.Add("data-updatedtargetid", this.UpdatedTargetId);
            }


            if (!String.IsNullOrWhiteSpace(this.InnerText))
            {
                if (!String.IsNullOrWhiteSpace(IconClassName))
                {
                    this.Builder.InnerHtml.AppendFormat(@"<i class=""{0}""></i> {1}", this.IconClassName, this.InnerText);
                }
                else
                {

                    this.Builder.InnerHtml.AppendHtml(this.InnerText);
                }
            }
        }

        public override string GetTag()
        {
            return "button";
        }
    }
}
