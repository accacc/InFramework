using IF.Web.Mvc.FluentHtml.Link;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTabLink : ActionLink
    {
        public bool Active { get; set; }
        public string DataTarget { get; set; }
        public bool IsAjax { get; set; }

        public Func<object, object>  Content { get; set; }



        public BootstrapTabLink(IHtmlHelper htmlHelper, string Text, string ActionName, string ControllerName)
            : base(htmlHelper, Text, ActionName, ControllerName)
        {

        }






    }
}
