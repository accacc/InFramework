using System.Web.Mvc;

namespace IF.Core.Mvc.PageLayout.FilterBox
{
    public class FilterBox
    {
        public bool IsVisible { get; set; }
        public string TemplateName { get; set; }
        public MvcHtmlString InnerHtml { get; set; }
    }
}
