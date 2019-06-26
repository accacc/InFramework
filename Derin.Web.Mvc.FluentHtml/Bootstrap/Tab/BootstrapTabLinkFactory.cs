using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTabLinkFactory
    {
        private BootstrapTab tab;

        public BootstrapTabLinkFactory(BootstrapTab tab)
        {
            this.tab = tab;
        }

        public virtual BootstrapTabLinkBuilder Add()
        {
            BootstrapTabLink tabStripItem = new BootstrapTabLink(this.tab.htmlHelper, String.Empty, String.Empty, String.Empty);
            this.tab.Items.Add(tabStripItem);
            return new BootstrapTabLinkBuilder(tab.htmlHelper,tabStripItem);
        }

    }
}
