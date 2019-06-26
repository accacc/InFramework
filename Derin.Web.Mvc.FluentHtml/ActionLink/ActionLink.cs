using IF.Core.Mvc;
using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Link
{
    public class ActionLink : HtmlRouteableElement
    {
        public string Text { get; set; }
        public string QueryString { get; set; }

        public string IconClassName { get; set; }

        public string GridRouteColumns { get; set; }

        public ActionWidgetRenderType RenderType { get; set; }

        public ActionLink(HtmlHelper htmlHelper, string Text, string ActionName, string ControllerName)
            : base(htmlHelper, ActionName, ControllerName)
        {
            this.Text = Text;
            this.RenderType = ActionWidgetRenderType.Icon;
        }

        public override MvcHtmlString CreateHtml()
        {
            
            
            //this.Builder.AddCssClass(this.CssClass);

            //this.Builder.Attributes.Add("id", Id);
        
            if(!String.IsNullOrWhiteSpace(IconClassName))
            {
                this.Builder.InnerHtml += String.Format(@"<span class=""{0}""></span>", this.IconClassName);
            }

            this.Builder.InnerHtml += this.Text;

            this.Builder.MergeAttributes(this.HtmlAttributes, true);

            string href = UrlHelper.GenerateUrl(null, this.ActionName, this.ControllerName, this.RouteValues, RouteTable.Routes, this.htmlHelper.ViewContext.RequestContext, false);

            this.Builder.Attributes.Add("href", href + QueryString);

            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }

        public override string GetTag()
        {
            return "a";
        }
    }
}

