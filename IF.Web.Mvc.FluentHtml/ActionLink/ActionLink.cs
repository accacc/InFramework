using IF.Core.Mvc;
using IF.Web.Mvc.FluentHtml.Base;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Mvc;

namespace IF.Web.Mvc.FluentHtml.Link
{
    public class ActionLink : HtmlRouteableElement
    {
        public string Text { get; set; }
        public string QueryString { get; set; }

        public string IconClassName { get; set; }

        public string GridRouteColumns { get; set; }

        public ActionWidgetRenderType RenderType { get; set; }

        public ActionLink(IHtmlHelper htmlHelper, string Text, string ActionName, string ControllerName)
            : base(htmlHelper, ActionName, ControllerName)
        {
            this.Text = Text;
            this.RenderType = ActionWidgetRenderType.Icon;
        }

        public override HtmlString CreateHtml()
        {
            
            
            //this.Builder.AddCssClass(this.CssClass);

            //this.Builder.Attributes.Add("id", Id);
        
            if(!String.IsNullOrWhiteSpace(IconClassName))
            {
                this.Builder.InnerHtml.Append(String.Format(@"<span class=""{0}""></span>", this.IconClassName));
            }

            this.Builder.InnerHtml.Append(this.Text);

            this.Builder.MergeAttributes(this.HtmlAttributes, true);

            var urlHelperFactory = (IUrlHelperFactory)htmlHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));

            var url = urlHelperFactory.GetUrlHelper(htmlHelper.ViewContext);


            string href = url.Action(this.ActionName, this.ControllerName, this.RouteValues);

            this.Builder.Attributes.Add("href", href + QueryString);

            return this.Builder.Render();
        }

        public override string GetTag()
        {
            return "a";
        }
    }
}

