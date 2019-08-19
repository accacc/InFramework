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

        public BootstrapAjaxFilterForm(IHtmlHelper htmlHelper, int ModelId)
            : base(htmlHelper, ModelId)
        {
        }


        protected override void InitalizeButtons()
        {
            this.Buttons = new List<IFButton>();
            this.DefaultSubmitButton = new IFButton(this.htmlHelper, this);
            this.DefaultSubmitButton.CssClass = "btn btn-primary";
            this.DefaultSubmitButton.InnerText = "Listele";
            this.DefaultSubmitButton.Id = "GridFilterButton";
            this.Buttons.Add(this.DefaultSubmitButton);

        }


        public override HtmlString CreateHtml()
        {
            return RenderForm();
        }

        protected override void RenderBody()
        {

            var writer = new System.IO.StringWriter();
            
            writer.WriteContent<object>(this.ContentAction, HtmlEncoder.Default, null, false);
            var content = new HtmlString(writer.ToString());
            this.Builder.InnerHtml.AppendHtml(content);
            this.Builder.InnerHtml.AppendHtml(this.RenderButtons());
            if (this.GenerateHiddenId)
            {
                this.Builder.InnerHtml.AppendHtml(this.RenderHiddenModelId());
            }
        }

        protected override HtmlString RenderButtons()
        {
            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("form-actions right");

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


    public class BootstrapAjaxGridForm : HtmlFormBase
    {

        public BootstrapAjaxGridForm(HtmlHelper htmlHelper, int ModelId)
            : base(htmlHelper, ModelId)
        {
        }


        protected override void InitalizeButtons()
        {
            this.Buttons = new List<IFButton>();
            this.DefaultSubmitButton = new IFButton(this.htmlHelper, this);
            this.DefaultSubmitButton.CssClass = "btn btn-primary";
            this.DefaultSubmitButton.InnerText = "Save";
            this.DefaultSubmitButton.Id = "GridFormButton";
            this.Buttons.Add(this.DefaultSubmitButton);

        }


        public override HtmlString CreateHtml()
        {
            return RenderForm();
        }

        protected override void RenderBody()
        {
            this.Builder.InnerHtml.AppendHtml(this.Builder.InnerHtml);
            this.Builder.InnerHtml.AppendHtml(this.Content);
            this.Builder.InnerHtml.AppendHtml(this.RenderButtons());
            if (this.GenerateHiddenId)
            {
                this.Builder.InnerHtml.AppendHtml(this.RenderHiddenModelId());
            }

        }

        protected override HtmlString RenderButtons()
        {
            TagBuilder actionsDiv = new TagBuilder("div");

            //actionsDiv.AddCssClass("form-actions right");

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

    public static class TextWriterExtensions
    {
        public static void WriteContent<T>(this StringWriter writer, Func<T, object> action, HtmlEncoder htmlEncoder, T dataItem = null, bool htmlEncode = false)
        where T : class
        {
            object obj = action(dataItem);
            HelperResult helperResult = obj as HelperResult;
            if (helperResult != null)
            {
                helperResult.WriteTo(writer, htmlEncoder);
                return;
            }
            if (obj != null)
            {
                if (htmlEncode)
                {
                    writer.Write(htmlEncoder.Encode(obj.ToString()));
                    return;
                }
                writer.Write(obj.ToString());
            }
        }
    }

}