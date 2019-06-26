using System.ComponentModel;
using System.Web.Mvc;

namespace IF.Core.Mvc.PageLayout.FilterBox
{
    public class FilterBoxFactory
    {


        internal FilterBox filterBox;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HtmlHelper htmlHelper
        {
            get;
            set;
        }

        public FilterBoxFactory(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.filterBox = new FilterBox();
        }

        public FilterBoxFactory IsVisible(bool IsVisible)
        {
            this.filterBox.IsVisible = IsVisible;
            return this;
        }

        public FilterBoxFactory TemplateName(string TemplateName)
        {
            this.filterBox.TemplateName = TemplateName;
            return this;
        }

        public FilterBoxFactory InnerHtml(MvcHtmlString InnerHtml)
        {
            this.filterBox.InnerHtml = InnerHtml;
            return this;
        }
    }
}
