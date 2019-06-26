using IF.Core.Mvc.PageLayout.SubmitButton;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class BootstrapAjaxFilterForm : HtmlFormBase
    {

        public BootstrapAjaxFilterForm(HtmlHelper htmlHelper, int ModelId)
            : base(htmlHelper, ModelId)
        {
        }


        protected override void InitalizeButtons()
        {
            this.Buttons = new List<Button>();
            this.DefaultSubmitButton = new Button(this.htmlHelper, this);
            this.DefaultSubmitButton.CssClass = "btn btn-primary";
            this.DefaultSubmitButton.InnerText = "Listele";
            this.DefaultSubmitButton.Id = "GridFilterButton";
            this.Buttons.Add(this.DefaultSubmitButton);

        }


        public override MvcHtmlString CreateHtml()
        {
            return RenderForm();
        }

        protected override void RenderBody()
        {
            this.Builder.InnerHtml = this.Builder.InnerHtml + this.Content;
            this.Builder.InnerHtml += this.RenderButtons();
            if (this.GenerateHiddenId)
            {
                this.Builder.InnerHtml += this.RenderHiddenModelId();
            }
        }

        protected override string RenderButtons()
        {
            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("form-actions right");

            foreach (Button button in this.Buttons)
            {
                if (String.IsNullOrWhiteSpace(button.TemplateName) && !button.Hide)
                {
                    actionsDiv.InnerHtml += button.Render().ToHtmlString();
                }
            }

            return actionsDiv.ToString(TagRenderMode.Normal);
        }


    }


    public class BootstrapAjaxGridForm : HtmlFormBase
    {

        public BootstrapAjaxGridForm(HtmlHelper htmlHelper, int ModelId)
            : base(htmlHelper, ModelId)
        {
        }


        protected override void InitalizeButtons()
        {
            this.Buttons = new List<Button>();
            this.DefaultSubmitButton = new Button(this.htmlHelper, this);
            this.DefaultSubmitButton.CssClass = "btn btn-primary";
            this.DefaultSubmitButton.InnerText = "Save";
            this.DefaultSubmitButton.Id = "GridFormButton";
            this.Buttons.Add(this.DefaultSubmitButton);

        }


        public override MvcHtmlString CreateHtml()
        {
            return RenderForm();
        }

        protected override void RenderBody()
        {
            this.Builder.InnerHtml = this.Builder.InnerHtml + this.Content;
            this.Builder.InnerHtml += this.RenderButtons();
            if (this.GenerateHiddenId)
            {
                this.Builder.InnerHtml += this.RenderHiddenModelId();
            }

        }

        protected override string RenderButtons()
        {
            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("form-actions right");

            foreach (Button button in this.Buttons)
            {
                if (String.IsNullOrWhiteSpace(button.TemplateName) && !button.Hide)
                {
                    actionsDiv.InnerHtml += button.Render().ToHtmlString();
                }
            }

            return actionsDiv.ToString(TagRenderMode.Normal);
        }
    }

}