using IF.Web.Mvc.FluentHtml.Base;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Modal.Bootstrap
{
    public class ModalBuilder
    {
        public Modal HtmlElement { get; set; }
        public IHtmlHelper htmlHelper { get; set; }

        public ModalBuilder(IHtmlHelper htmlHelper)
        {
            this.HtmlElement = new Modal(htmlHelper);
        }



        public ModalBuilder Content(IHtmlContent Content)
        {
            var element = new HtmlDivElement(this.htmlHelper);
            element.Build();
            element.Builder.InnerHtml.AppendHtml(Content);

            this.HtmlElement.Childs.Add(element);
            return this;
        }

        public ModalBuilder Title(string Title)
        {
            this.HtmlElement.Title = Title;
            return this;
        }

        public ModalBuilder AcceptChild(IHtmlElement child)
        {
            this.HtmlElement.Childs.Add(child);
            return this;
        }


        public ModalBuilder CssClass(string CssClass)
        {
            this.HtmlElement.CssClass = CssClass;
            return this;
        }

        public ModalBuilder Id(string Id)
        {
            this.HtmlElement.Id = Id;
            return this;
        }


        public HtmlString Render()
        {
            return this.HtmlElement.Render();
        }



        public ModalBuilder HtmlAttributes(object HtmlAttributes)
        {
            var attributes = new RouteValueDictionary(HtmlAttributes);

            foreach (var attribute in attributes)
            {
                this.HtmlElement.HtmlAttributes.Add(attribute.Key, attribute.Value);
            }

            return this;
        }



    }
}
