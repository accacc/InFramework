using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
