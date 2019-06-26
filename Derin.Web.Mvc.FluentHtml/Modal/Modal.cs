using IF.Web.Mvc.FluentHtml.Base;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Modal
{
    public class Modal : HtmlContainerElement
    {
        public Modal(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
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

        public override MvcHtmlString CreateHtml()
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

            buttonImage.InnerHtml = "&times;";

            this.CloseButton.Build();

            this.CloseButton.Builder.InnerHtml = buttonImage.ToString(TagRenderMode.Normal);

            TagBuilder h2 = new TagBuilder("h2");

            h2.AddCssClass("modal-title");

            TagBuilder b = new TagBuilder("b");

            b.InnerHtml = this.Title;

            h2.InnerHtml = b.ToString(TagRenderMode.Normal);

            header.InnerHtml = this.CloseButton.Builder.ToString(TagRenderMode.Normal) + h2.ToString(TagRenderMode.Normal);


            TagBuilder body = new TagBuilder("div");

            body.AddCssClass("modal-body");

            foreach (var child in this.Childs)
            {
                body.InnerHtml += child.Render();
            }

            content.InnerHtml = header.ToString(TagRenderMode.Normal) + body.ToString(TagRenderMode.Normal);

            this.Builder.InnerHtml = content.ToString(TagRenderMode.Normal);



            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));


        }

        public override string GetTag()
        {
            return ("div");
        }


    }
}
