//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.Web.Mvc.FluentHtml.HtmlForm
//{
//    public class BootstrapAjaxGridForm : HtmlFormBase
//    {

//        public BootstrapAjaxGridForm(HtmlHelper htmlHelper, int ModelId)
//            : base(htmlHelper, ModelId)
//        {
//        }


//        protected override void InitalizeButtons()
//        {
//            this.Buttons = new List<IFButton>();
//            this.SubmitButton = new IFButton(this.htmlHelper, this);
//            this.SubmitButton.CssClass = "btn btn-primary";
//            this.SubmitButton.InnerText = "Save";
//            this.SubmitButton.Id = "GridFormButton";
//            this.Buttons.Add(this.SubmitButton);

//        }


//        public override HtmlString CreateHtml()
//        {
//            return RenderForm();
//        }

//        protected override void RenderBody()
//        {
//            this.Builder.InnerHtml.AppendHtml(this.Builder.InnerHtml);
//            try
//            {
//                var writer = new System.IO.StringWriter();

//                writer.WriteContent<object>(this.ContentAction, HtmlEncoder.Default, null, false);
//                var c = new HtmlString(writer.ToString());
//                this.Builder.InnerHtml.AppendHtml(c);
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//            this.Builder.InnerHtml.AppendHtml(this.RenderButtons());
//            if (this.GenerateHiddenId)
//            {
//                this.Builder.InnerHtml.AppendHtml(this.RenderHiddenModelId());
//            }

//        }

//        protected override HtmlString RenderButtons()
//        {
//            TagBuilder actionsDiv = new TagBuilder("div");

//            //actionsDiv.AddCssClass("form-actions right");

//            foreach (IFButton button in this.Buttons)
//            {
//                if (String.IsNullOrWhiteSpace(button.TemplateName) && !button.Hide)
//                {
//                    actionsDiv.InnerHtml.AppendHtml(button.Render());
//                }
//            }

//            return actionsDiv.Render(TagRenderMode.Normal);
//        }
//    }
//}
