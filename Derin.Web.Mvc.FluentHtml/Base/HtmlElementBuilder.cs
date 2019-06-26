using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Base
{

    public abstract class HtmlElementBuilder<Element> where Element : IHtmlElement
    {
        public Element HtmlElement { get; set; }
    }

    public abstract class HtmlElementBuilder<Element, Builder> : HtmlElementBuilder<Element>
        where Element : IHtmlElement
        where Builder : HtmlElementBuilder<Element, Builder>
    {


        
        public Builder CssClass(string CssClass)
        {
            this.HtmlElement.CssClass = CssClass;
            return this as Builder;
        }

        public Builder Id(string Id)
        {
            this.HtmlElement.Id = Id;
            return this as Builder;
        }   
    

        public MvcHtmlString Render()
        {
            return this.HtmlElement.Render();
        }

       

        public Builder HtmlAttributes(object HtmlAttributes)
        {
            var attributes = new RouteValueDictionary(HtmlAttributes);

            foreach (var attribute in attributes)
            {
                this.HtmlElement.HtmlAttributes.Add(attribute.Key, attribute.Value);
            }

            return this as Builder;
        }

    }
}
