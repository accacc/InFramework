using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{

    public abstract class HtmlFormBase : HtmlElement
    {

        public string Name { get; set; }
        public IList<IFButton> Buttons { get; set; }
        public IFButton SubmitButton { get; set; }

        public Func<object, object> ContentAction { get; set; }
        public bool NavigationButtons { get; set; }
        public string Title { get; set; }
        public ButtonPosition DefaultButtonPosition { get; set; }


        public HtmlFormBase(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.htmlHelper.ViewContext.FormContext = new FormContext();
            //this.ModelId = ModelId;
            this.Name = this.Id;
            this.DefaultButtonPosition = ButtonPosition.BottomRight;
            //this.GenerateHiddenId = true;
            //this.InitalizeButtons();
        }

        public override string GetTag()
        {
            return "form"; ;
        }

        //protected abstract void InitalizeButtons();
        //protected abstract void RenderBody();
        //protected abstract HtmlString RenderButtons();

        //public string RenderHiddenModelId()
        //{

        //    TagBuilder modelHiddenIdHtml = new TagBuilder("input");
        //    modelHiddenIdHtml.Attributes.Add("type", "hidden");
        //    modelHiddenIdHtml.Attributes.Add("name", "Id");
        //    modelHiddenIdHtml.Attributes.Add("value", ModelId.ToString());
        //    return modelHiddenIdHtml.Render().ToString();
        //}

        //protected virtual HtmlString RenderForm()
        //{

        //    //this.Builder.MergeAttributes(this.HtmlAttributes);           

        //    //if (String.IsNullOrWhiteSpace(this.Method))
        //    //{
        //    //    this.Method = "POST";
        //    //}

        //    //this.Builder.Attributes.Add("method", Method);            

        //    RenderBody();

        //    foreach (IFButton button in this.Buttons)
        //    {
        //        if (!String.IsNullOrWhiteSpace(button.TemplateName))
        //        {
        //            this.Builder.InnerHtml.Append(this.Builder.InnerHtml.ToString().Replace("${" + button.TemplateName + "}", button.Render().Value));
        //        }
        //    }


        //    return this.Builder.Render();
        //}
    }
}
