using IF.Web.Mvc.FluentHtml.Button;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class HtmlForm : HtmlFormBase
    {



        public HtmlForm(IHtmlHelper htmlHelper, object ModelId)
            : base(htmlHelper, ModelId)
        {
        }

        protected override void InitalizeButtons()
        {
            this.Buttons = new List<IFButton>();
            this.DefaultSubmitButton = new IFButton(this.htmlHelper, this);
            this.DefaultSubmitButton.CssClass = "btn btn-primary";
            this.DefaultSubmitButton.InnerText = "Kaydet";
            //this.DefaultSubmitButton.IconClassName = "fa fa-plus";
            this.Buttons.Add(this.DefaultSubmitButton);


            this.DefaultCancelButton = new IFButton(this.htmlHelper, this);
            this.DefaultCancelButton.InnerText = "İptal";
            this.DefaultCancelButton.CssClass = "btn btn-default";
            this.DefaultCancelButton.Type = "button";
            this.DefaultCancelButton.Id = "DefaultCancelButton";
            //this.DefaultCancelButton.IconClassName = "fa fa-times";
            //this.DefaultCancelButton.HtmlAttributes.Add("data-dismiss", "modal");
            this.DefaultCancelButton.ActionName = "Index";
            this.Buttons.Add(this.DefaultCancelButton);
        }

        public override HtmlString CreateHtml()
        {
            return RenderForm();
        }

        protected override void RenderBody()
        {


           

            this.Builder.InnerHtml.AppendHtml("<div class=\"form-body\">");

            try
            {
                var writer = new System.IO.StringWriter();

                writer.WriteContent<object>(this.ContentAction, HtmlEncoder.Default, null, false);
                var c = new HtmlString(writer.ToString());
                this.Builder.InnerHtml.AppendHtml(c);
            }
            catch (Exception ex)
            {

                throw;
            }

            this.Builder.InnerHtml.AppendHtml("</div>");

            this.Builder.InnerHtml.AppendHtml(this.RenderButtons());

            this.Builder.InnerHtml.AppendHtml("<br /><br />");

            if (this.ModelId != null)
            {
                if (this.GenerateHiddenId)
                {
                    this.Builder.InnerHtml.AppendHtml(this.RenderHiddenModelId());
                }
            }


        }

        protected override HtmlString RenderButtons()
        {

            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("modal-footer");
            //actionsDiv.Attributes.Add("role","group");

            //TagBuilder actionLeft = new TagBuilder("div");

            foreach (IFButton button in this.Buttons)
            {
                if (String.IsNullOrWhiteSpace(button.TemplateName) && !button.Hide)
                {
                    if (this.DefaultButtonPosition == DefaultButtonPosition.BottomRight)
                    {
                        actionsDiv.AddCssClass("pull-right");
                    }
                    else
                    {
                        actionsDiv.AddCssClass("pull-left");
                    }

                    actionsDiv.InnerHtml.AppendHtml(button.Render());
                    this.Builder.InnerHtml.Append("&nbsp;");
                }
            }



            return actionsDiv.Render(TagRenderMode.Normal);
        }


    }
}
