using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace IF.Web.Mvc.FluentHtml.Bootstrap
{
    public class BootstrapRow : IDisposable
    {
        protected HtmlHelper helper;

        public BootstrapRow(HtmlHelper helper)
        {
            this.helper = helper;
            helper.ViewContext.Writer.Write(@"<div class=""row"">");
        }

        public void Dispose()
        {
            helper.ViewContext.Writer.Write("</div>");
        }
    }
}
