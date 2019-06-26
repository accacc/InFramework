using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public class HtmlContainerElement : HtmlElement,IContainerElement
    {

        public HtmlContainerElement(HtmlHelper htmlHelper)
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

       
        

        public override MvcHtmlString CreateHtml()
        {

            foreach (var child in this.Childs)
            {
                this.Builder.InnerHtml += child.Render();
            }

            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }

        public override string GetTag()
        {
            return "div";
        }


        
    }
}
