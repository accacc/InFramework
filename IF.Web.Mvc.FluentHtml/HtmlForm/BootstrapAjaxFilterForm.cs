using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Button;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class BootstrapAjaxFilterForm : HtmlFormBase
    {
        public BootstrapAjaxFilterForm(IHtmlHelper htmlHelper)   : base(htmlHelper)
        {
            this.htmlHelper.ViewContext.FormContext = new FormContext();
            //this.ModelId = ModelId;
            this.Name = this.Id;
            this.DefaultButtonPosition = ButtonPosition.BottomRight;
            //this.GenerateHiddenId = true;
            this.InitalizeButtons();
        }

      
        protected  void InitalizeButtons()
        {
            this.Buttons = new List<IFButton>();
            this.SubmitButton = new IFButton(this.htmlHelper, this);
            this.SubmitButton.CssClass = "btn btn-primary";
            this.SubmitButton.InnerText = "Listele";
            this.SubmitButton.Id = "GridFilterButton";
            this.Buttons.Add(this.SubmitButton);

        }


        public override HtmlString CreateHtml()
        {          

            var writer = new System.IO.StringWriter();            
            writer.WriteContent<object>(this.ContentAction, HtmlEncoder.Default, null, false);
            var content = new HtmlString(writer.ToString());
            this.Builder.InnerHtml.AppendHtml(content);
            this.Builder.InnerHtml.AppendHtml(this.RenderButtons());

            return this.Builder.Render();

        }

        protected HtmlString RenderButtons()
        {
            TagBuilder actionsDiv = new TagBuilder("div");

            foreach (IFButton button in this.Buttons)
            {
                if (String.IsNullOrWhiteSpace(button.TemplateName) && !button.Hide)
                {
                    actionsDiv.InnerHtml.AppendHtml(button.Render());
                }
            }

            return actionsDiv.Render(TagRenderMode.Normal);
        }


    }


   

    

}