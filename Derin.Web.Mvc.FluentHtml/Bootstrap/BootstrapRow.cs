using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
