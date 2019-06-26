using IF.Web.Mvc.FluentHtml.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTabLink : ActionLink
    {
        public bool Active { get; set; }
        public string DataTarget { get; set; }
        public bool IsAjax { get; set; }

        public string Content { get; set; }



        public BootstrapTabLink(HtmlHelper htmlHelper, string Text, string ActionName, string ControllerName)
            : base(htmlHelper, Text, ActionName, ControllerName)
        {

        }






    }
}
