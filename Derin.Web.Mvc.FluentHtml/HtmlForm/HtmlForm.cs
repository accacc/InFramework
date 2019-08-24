using IF.Core.Mvc.PageLayout.SubmitButton;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class HtmlForm : HtmlFormBase
    {



        public HtmlForm(HtmlHelper htmlHelper, object ModelId)
            : base(htmlHelper, ModelId)
        {
        }

        protected override void InitalizeButtons()
        {
            this.Buttons = new List<Button>();
            this.DefaultSubmitButton = new Button(this.htmlHelper, this);
            this.DefaultSubmitButton.CssClass = "btn btn-primary";
            this.DefaultSubmitButton.InnerText = "Kaydet";
            //this.DefaultSubmitButton.IconClassName = "fa fa-plus";
            this.Buttons.Add(this.DefaultSubmitButton);


            this.DefaultCancelButton = new Button(this.htmlHelper, this);
            this.DefaultCancelButton.InnerText = "İptal";
            this.DefaultCancelButton.CssClass = "btn btn-default";
            this.DefaultCancelButton.Type = "button";
            //this.DefaultCancelButton.Id = "DefaultCancelButton";
            //this.DefaultCancelButton.IconClassName = "fa fa-times";
            this.DefaultCancelButton.HtmlAttributes.Add("data-dismiss", "modal");
            this.DefaultCancelButton.ActionName = "Index";
            this.Buttons.Add(this.DefaultCancelButton);
        }

        public override MvcHtmlString CreateHtml()
        {
            return RenderForm();
        }

        protected override void RenderBody()
        {


            this.Builder.InnerHtml = this.Builder.InnerHtml + "<div class=\"form-body\">" + this.Content + "</div>";

            this.Builder.InnerHtml += this.RenderButtons() + "<br /><br />";

            if (this.ModelId != null)
            {
                if (this.GenerateHiddenId)
                {
                    this.Builder.InnerHtml += this.RenderHiddenModelId();
                }
            }


        }

        protected override string RenderButtons()
        {

            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("modal-footer");
            //actionsDiv.Attributes.Add("role","group");

            //TagBuilder actionLeft = new TagBuilder("div");

            foreach (Button button in this.Buttons)
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

                    actionsDiv.InnerHtml += button.Render().ToHtmlString() + "&nbsp;";
                }
            }



            return actionsDiv.ToString(TagRenderMode.Normal);
        }


    }
}
