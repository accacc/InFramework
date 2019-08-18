using IF.Web.Mvc.FluentHtml.Base;

namespace IF.Web.Mvc.FluentHtml.Link
{


    public class ActionLinkSpecialFactory
    {

      
        IActionLinkableElement element;
        public ActionLinkSpecialFactory(IActionLinkableElement element)
        {
            this.element = element;
        }

        public AjaxLinkBuilder Add(AjaxLinkBuilder builder)
        {
            this.element.ActionLinks.Add(builder);
            return builder;
        } 
    }
}
