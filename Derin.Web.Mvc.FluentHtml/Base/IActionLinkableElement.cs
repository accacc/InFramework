using IF.Web.Mvc.FluentHtml.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public interface  IActionLinkableElement
    {
        IList<AjaxLinkBuilder> ActionLinks { get; set; }
    }
}
