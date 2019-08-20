using IF.Web.Mvc.FluentHtml.Base;
using System;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTabBuilder : HtmlElementBuilder<BootstrapTab, BootstrapTabBuilder>
    {


        public BootstrapTabBuilder(BootstrapTab tab)
        {
            this.HtmlElement = tab;
        }
        public BootstrapTabBuilder Items(Action<BootstrapTabLinkFactory> addAction)
        {

            addAction(new BootstrapTabLinkFactory(this.HtmlElement));
            return this;
        }


    }
}
