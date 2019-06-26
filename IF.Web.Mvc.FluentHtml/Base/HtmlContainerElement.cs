using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public class HtmlContainerElement : HtmlElement, IContainerElement
    {

        public HtmlContainerElement(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.Childs = new List<IHtmlElement>();
        }

        public IList<IHtmlElement> Childs
        {
            get;
            private set;
        }

        public void AcceptChild(IHtmlElement child)
        {
            this.Childs.Add(child);
        }




        public override HtmlString CreateHtml()
        {

            foreach (var child in this.Childs)
            {
                this.Builder.InnerHtml.AppendHtml(child.Render());
            }

            return this.Builder.Render();
        }

        public override string GetTag()
        {
            return "div";
        }



    }
}
