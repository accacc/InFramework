using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Extension;
using IF.Web.Mvc.FluentHtml.HtmlForm;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

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



        public ModalBuilder Content(Func<object, object> content)
        {
            var element = new HtmlDivElement(this.htmlHelper);
            element.Build();

            var writer = new System.IO.StringWriter();

            writer.WriteContent<object>(content, HtmlEncoder.Default, null, false);
            var c = new HtmlString(writer.ToString());

            element.Builder.InnerHtml.AppendHtml(c);

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
