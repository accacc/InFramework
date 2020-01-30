using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Modal.Bootstrap
{
    public class Modal : HtmlContainerElement
    {
        public Modal(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public Action ContentAction
        {
            get;
            set;
        }

        public ModalButton CloseButton { get; set; }

        public string Title { get; set; }


        //        <div class="modal-dialog" role="document">
        //    <div class="modal-content">
        //        <div class="modal-header">
        //            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        //        </div>
        //        <div class="modal-body">
        //Child
        //        </div>
        //    </div>
        //</div>

        public override HtmlString CreateHtml()
        {

            this.Builder.AddCssClass("modal-dialog");
            //modal.Attributes.Add("id", "TargetdialogScreen");
            //this.Builder.Attributes.Add("role", "dialog");

            TagBuilder content = new TagBuilder("div");
            content.AddCssClass("modal-content");

            TagBuilder header = new TagBuilder("div");
            header.AddCssClass("modal-header");

            this.CloseButton = new ModalButton(htmlHelper);
            this.CloseButton.CssClass = "close";
            this.CloseButton.Type = "button";
            this.CloseButton.DataDismiss = "modal";
            this.CloseButton.AriaLabel = "Close";



            TagBuilder buttonImage = new TagBuilder("span");

            buttonImage.Attributes.Add("aria-hidden", "true");

            buttonImage.InnerHtml.AppendHtml("&times;");

            this.CloseButton.Build();

            this.CloseButton.Builder.InnerHtml.AppendHtml(buttonImage);

            TagBuilder title = new TagBuilder("h2");

            title.AddCssClass("modal-title");

            TagBuilder b = new TagBuilder("b");

            b.InnerHtml.AppendHtml(this.Title);

            title.InnerHtml.AppendHtml(b);          
            
            header.InnerHtml.AppendHtml(title);

            header.InnerHtml.AppendHtml(this.CloseButton.Builder);


            TagBuilder body = new TagBuilder("div");

            body.AddCssClass("modal-body");

            foreach (var child in this.Childs)
            {
                body.InnerHtml.AppendHtml(child.Render());
            }

            content.InnerHtml.AppendHtml(header);
            content.InnerHtml.AppendHtml(body);

            this.Builder.InnerHtml.AppendHtml(content);



            return this.Builder.Render();


        }

        public override string GetTag()
        {
            return ("div");
        }


    }
}
