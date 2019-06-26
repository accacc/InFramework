using System.Web.Mvc;

namespace IF.Core.Mvc.PageLayout.PageLayout
{
    public class PageLayout
    {

        public IF.Core.Mvc.PageLayout.FilterBox.FilterBox FilterBox { get; set; }
        public string DataSectionTemplateName { get; set; }
        public string PageInfoSectionTemplateName { get; set; }
        public string ButtonSectionTemplateName { get; set; }
        public MvcHtmlString DataSectionName { get; set; }
        public MvcHtmlString PageInfoSectionName { get; set; }
        public MvcHtmlString ButtonSectionName { get; set; }
    }
}
