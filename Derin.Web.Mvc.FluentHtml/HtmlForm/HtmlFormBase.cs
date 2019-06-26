using IF.Core.Mvc.PageLayout.SubmitButton;
using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public enum DefaultButtonPosition
    {
        TopLeft = 0,
        TopRight = 1,
        BottomLeft = 2,
        BottomRight = 3

    }
    public abstract class HtmlFormBase : HtmlRouteableElement
    {

        public string Name { get; set; }
        public IList<Button> Buttons { get; set; }
        public Button DefaultSubmitButton { get; set; }
        public Button DefaultCancelButton { get; set; }
        public string Content { get; set; }
        public bool NavigationButtons { get; set; }
        public object ModelId { get; set; }
        public string Title { get; set; }
        public string Method { get; set; }

        public bool GenerateHiddenId { get; set; }

        public DefaultButtonPosition DefaultButtonPosition { get; set; }


        public HtmlFormBase(HtmlHelper htmlHelper, object ModelId)
            : base(htmlHelper)
        {
            this.htmlHelper.ViewContext.FormContext = new FormContext();
            this.ModelId = ModelId;
            this.Name = this.Id;
            this.DefaultButtonPosition = DefaultButtonPosition.BottomRight;
            this.GenerateHiddenId = true;
            this.InitalizeButtons();
        }

        public override string GetTag()
        {
            return "form"; ;
        }

        protected abstract void InitalizeButtons();
        protected abstract void RenderBody();
        protected abstract string RenderButtons();

        public string RenderHiddenModelId()
        {

            TagBuilder modelHiddenIdHtml = new TagBuilder("input");
            modelHiddenIdHtml.Attributes.Add("type", "hidden");
            modelHiddenIdHtml.Attributes.Add("name", "Id");
            modelHiddenIdHtml.Attributes.Add("value", ModelId.ToString());
            return modelHiddenIdHtml.ToString(TagRenderMode.Normal);
        }

        protected virtual MvcHtmlString RenderForm()
        {

            this.Builder.MergeAttributes(this.HtmlAttributes);

            //if (this.NavigationButtons && this.ModelId > 0)
            //{
            //    form.InnerHtml += RenderNavigationButtons() + "<br />";
            //}


            //this.Builder.Attributes.Add("id", this.Id);

            if (String.IsNullOrWhiteSpace(this.Method))
            {
                this.Method = "POST";
            }

            this.Builder.Attributes.Add("method", Method);

            string href = UrlHelper.GenerateUrl("Default", this.ActionName, this.ControllerName, this.RouteValues, RouteTable.Routes, this.htmlHelper.ViewContext.RequestContext, false);

            this.Builder.Attributes.Add("action", href);

            RenderBody();

            foreach (Button button in this.Buttons)
            {
                if (!String.IsNullOrWhiteSpace(button.TemplateName))
                {
                    this.Builder.InnerHtml = this.Builder.InnerHtml.Replace("${" + button.TemplateName + "}", button.Render().ToHtmlString());
                }
            }

            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }
    }
}
