using IF.Core.Mvc.PageLayout.FilterBox;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace IF.Core.Mvc.PageLayout.PageLayout
{
    public class PageLayoutFactory
    {
        private PageLayout pageLayout = null;


        [EditorBrowsable(EditorBrowsableState.Never)]
        public HtmlHelper htmlHelper
        {
            get;
            set;
        }

        public PageLayoutFactory(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.pageLayout = new PageLayout();
        }


        public PageLayoutFactory FilterBox(Action<FilterBoxFactory> filterBox)
        {
            FilterBoxFactory filterBoxFactory = new FilterBoxFactory(this.htmlHelper);
            this.pageLayout.FilterBox = filterBoxFactory.filterBox;
            filterBox(filterBoxFactory);
            return this;
        }

        public PageLayoutFactory PageInfoSection(string PageInfoSectionTemplateName)
        {
            this.pageLayout.PageInfoSectionTemplateName = PageInfoSectionTemplateName;
            return this;
        }

        public PageLayoutFactory DataSection(string DataSectionTemplateName)
        {
            this.pageLayout.DataSectionTemplateName = DataSectionTemplateName;
            return this;
        }

        public PageLayoutFactory ButtonSetSection(string ButtonSectionTemplateName)
        {
            this.pageLayout.ButtonSectionTemplateName = ButtonSectionTemplateName;
            return this;
        }

        public MvcHtmlString Render()
        {
            StringBuilder sb = new StringBuilder();

            var ajaxHelper = new AjaxHelper(this.htmlHelper.ViewContext, this.htmlHelper.ViewDataContainer);


            if (this.pageLayout.FilterBox != null && this.pageLayout.FilterBox.IsVisible)
            {
                this.pageLayout.FilterBox.InnerHtml = this.htmlHelper.Partial(this.pageLayout.FilterBox.TemplateName);
            }

            //this.pageLayout.DataSectionName = this.htmlHelper.Partial(this.pageLayout.DataSectionTemplateName);
            this.pageLayout.DataSectionName = MvcHtmlString.Create(this.pageLayout.DataSectionTemplateName);
            //this.pageLayout.ButtonSectionName = this.htmlHelper.Partial(this.pageLayout.ButtonSectionTemplateName);
            this.pageLayout.PageInfoSectionName = this.htmlHelper.Partial(this.pageLayout.PageInfoSectionTemplateName);
            return this.htmlHelper.Partial("_PageLayout3", this.pageLayout);
        }

        private void RenderPartial(string partialName)
        {
            this.htmlHelper.RenderPartial(partialName);
        }

        private void WriteToPage(string data)
        {
            this.htmlHelper.ViewContext.Writer.Write(data);

        }

    }
}
