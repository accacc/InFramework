using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
