using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace IF.Web.Mvc.FluentHtml.Bootstrap
{


    public class BootstrapColumn : IDisposable
    {
        protected HtmlHelper helper;

        public BootstrapColumn(HtmlHelper helper)
        {
            this.helper = helper;
            helper.ViewContext.Writer.Write(@"<div class=""col-md-6"">");
        }



        public void Dispose()
        {
            helper.ViewContext.Writer.Write("</div>");
        }
    }

}
