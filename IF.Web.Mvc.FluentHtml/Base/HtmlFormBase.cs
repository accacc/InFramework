//using IF.Web.Mvc.FluentHtml.Button;
//using IF.Web.Mvc.FluentHtml.Extension;
//using Microsoft.AspNetCore.Html;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.Web.Mvc.FluentHtml.Base
//{
//    public abstract class HtmlFormBase : HtmlRouteableElement
//    {

//        public string Name { get; set; }
//        public IList<IFButton> Buttons { get; set; }
//        public IFButton DefaultSubmitButton { get; set; }
//        public IFButton DefaultCancelButton { get; set; }
//        public string Content { get; set; }
//        public bool NavigationButtons { get; set; }
//        public object ModelId { get; set; }
//        public string Title { get; set; }
//        public string Method { get; set; }

//        public bool GenerateHiddenId { get; set; }

//        public DefaultButtonPosition DefaultButtonPosition { get; set; }


//        public HtmlFormBase(IHtmlHelper htmlHelper, object ModelId)
//            : base(htmlHelper)
//        {
//            this.htmlHelper.ViewContext.FormContext = new FormContext();
//            this.ModelId = ModelId;
//            this.Name = this.Id;
//            this.DefaultButtonPosition = DefaultButtonPosition.BottomRight;
//            this.GenerateHiddenId = true;
//            this.InitalizeButtons();
//        }

//        public override string GetTag()
//        {
//            return "form"; ;
//        }

//        protected abstract void InitalizeButtons();
//        protected abstract void RenderBody();
//        protected abstract string RenderButtons();

//        public HtmlString RenderHiddenModelId()
//        {

//            TagBuilder modelHiddenIdHtml = new TagBuilder("input");
//            modelHiddenIdHtml.Attributes.Add("type", "hidden");
//            modelHiddenIdHtml.Attributes.Add("name", "Id");
//            modelHiddenIdHtml.Attributes.Add("value", ModelId.ToString());
//            return modelHiddenIdHtml.Render();
//        }

//        protected virtual HtmlString RenderForm()
//        {

//            this.Builder.MergeAttributes(this.HtmlAttributes);

//            //if (this.NavigationButtons && this.ModelId > 0)
//            //{
//            //    form.InnerHtml += RenderNavigationButtons() + "<br />";
//            //}


//            //this.Builder.Attributes.Add("id", this.Id);

//            if (String.IsNullOrWhiteSpace(this.Method))
//            {
//                this.Method = "POST";
//            }

//            this.Builder.Attributes.Add("method", Method);


//            //TODO:Caglar
//            string href = "aaa";//UrlHelper.GenerateUrl("Default", this.ActionName, this.ControllerName, this.RouteValues, RouteTable.Routes, this.htmlHelper.ViewContext.RequestContext, false);

//            this.Builder.Attributes.Add("action", href);

//            RenderBody();


//            //TODO:Caglar
//            //foreach (IFButton button in this.Buttons)
//            //{

//            //    if (!String.IsNullOrWhiteSpace(button.TemplateName))
//            //    {
//            //        this.Builder.InnerHtml.AppendHtml(this.Builder.InnerHtml.Replace("${" + button.TemplateName + "}", button.Render().ToHtmlString());
//            //    }
//            //}

//            return this.Builder.Render();
//        }
//    }
//}
