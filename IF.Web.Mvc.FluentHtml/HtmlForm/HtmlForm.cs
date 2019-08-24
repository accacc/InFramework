using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class IFHtmlForm : HtmlFormBase
    {
        public IFButton CancelButton { get; set; }
        //public IHtmlContent Content { get; set; }

        public IFHtmlForm(IHtmlHelper htmlHelper):base(htmlHelper)
        {
            this.htmlHelper.ViewContext.FormContext = new FormContext();
            //this.ModelId = ModelId;
            this.Name = this.Id;
            this.DefaultButtonPosition = ButtonPosition.BottomRight;
            //this.GenerateHiddenId = true;
            this.InitalizeButtons();
        }

        protected void InitalizeButtons()
        {
            this.Buttons = new List<IFButton>();
            this.SubmitButton = new IFButton(this.htmlHelper, this);
            this.SubmitButton.CssClass = "btn btn-primary";
            this.SubmitButton.InnerText = "Kaydet";
            //this.DefaultSubmitButton.IconClassName = "fa fa-plus";
            this.Buttons.Add(this.SubmitButton);


            this.CancelButton = new IFButton(this.htmlHelper, this);
            this.CancelButton.InnerText = "İptal";
            this.CancelButton.CssClass = "btn btn-danger";
            this.CancelButton.Type = "button";
            this.CancelButton.Id = "DefaultCancelButton";
            //this.DefaultCancelButton.IconClassName = "fa fa-times";
            this.CancelButton.HtmlAttributes.Add("data-dismiss", "modal");
            this.CancelButton.ActionName = "Index";
            this.Buttons.Add(this.CancelButton);
        }

        public override HtmlString CreateHtml()
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

                this.Builder.InnerHtml.Append("Content Render Fail! " + ex.GetBaseException().Message);
            }

            this.Builder.InnerHtml.AppendHtml("</div>");

            this.Builder.InnerHtml.AppendHtml(this.RenderButtons());

            this.Builder.InnerHtml.AppendHtml("<br /><br />");

            //if (this.ModelId != null)
            //{
            //    if (this.GenerateHiddenId)
            //    {
            //        this.Builder.InnerHtml.AppendHtml(this.RenderHiddenModelId());
            //    }
            //}

            return this.Builder.Render();
        }

        protected HtmlString RenderButtons()
        {

            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("modal-footer");
            //actionsDiv.Attributes.Add("role","group");

            //TagBuilder actionLeft = new TagBuilder("div");

            foreach (IFButton button in this.Buttons)
            {
                if (String.IsNullOrWhiteSpace(button.TemplateName) && !button.Hide)
                {
                    if (this.DefaultButtonPosition == ButtonPosition.BottomRight)
                    {
                        actionsDiv.AddCssClass("pull-right");
                    }
                    else
                    {
                        actionsDiv.AddCssClass("pull-left");
                    }

                    actionsDiv.InnerHtml.AppendHtml(button.Render());
                    this.Builder.InnerHtml.AppendHtml("&nbsp;");
                }
            }



            return actionsDiv.Render(TagRenderMode.Normal);
        }

        public override string GetTag()
        {
            return "form"; ;
        }
    }
}
