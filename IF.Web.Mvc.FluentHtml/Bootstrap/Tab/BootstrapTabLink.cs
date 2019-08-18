using IF.Web.Mvc.FluentHtml.Link;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
