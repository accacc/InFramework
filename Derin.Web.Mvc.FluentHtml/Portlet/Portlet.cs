using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Portlet
{
    public class Portlet : HtmlContainerElement
    {
        public Portlet(HtmlHelper htmlHelper):base(htmlHelper)
        {
        }



        public string Title { get; set; }

        public string Icon { get; set; }

        public override MvcHtmlString CreateHtml()
        {
            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));

            
        }

        public override void Build()
        {
            base.Build();

            
            var title = GetTitle();
            var caption = GetCaption();
            var icon = GetIcon();
            caption.InnerHtml = icon.ToString(TagRenderMode.Normal) + this.Title;
            title.InnerHtml = caption.ToString(TagRenderMode.Normal);

            var body = this.GetBody();

            if (!String.IsNullOrWhiteSpace(this.CssClass))
            {
                this.Builder.AddCssClass(this.CssClass);
            }
            else
            {
                this.Builder.AddCssClass("portlet light bordered");
            }

            this.Builder.InnerHtml = title.ToString(TagRenderMode.Normal) + body.ToString(TagRenderMode.Normal);
            
        }

        public TagBuilder GetBody()
        {
            TagBuilder body = new TagBuilder("div");
            body.AddCssClass("portlet-body form");
            
            foreach (var child in this.Childs)
            {
                body.InnerHtml += child.Builder.ToString(TagRenderMode.Normal);
            }
            
            return body;
        }

        
        private TagBuilder GetIcon()
        {
            TagBuilder icon = new TagBuilder("i");


            if (!String.IsNullOrWhiteSpace(this.Icon))
            {
                icon.AddCssClass(this.Icon);
            }
            else
            {
                icon.AddCssClass("fa fa-gift");
            }

            return icon;
        }

        private TagBuilder GetCaption()
        {
            TagBuilder caption = new TagBuilder("div");

            caption.AddCssClass("caption");

            return caption;
        }

        private TagBuilder GetTitle()
        {
            TagBuilder title = new TagBuilder("div");

            title.AddCssClass("portlet-title");

            return title;
        }


        public override string GetTag()
        {
            return ("div");
        }
    }
}
