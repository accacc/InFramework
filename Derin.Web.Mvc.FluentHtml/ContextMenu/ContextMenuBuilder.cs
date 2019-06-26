using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Link;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.ContextMenu
{
    public class ContextMenuBuilder: HtmlElementBuilder<ContextMenu.Menu, ContextMenuBuilder>
    {
        public ContextMenuBuilder(HtmlHelper htmlHelper, IEnumerable<ActionLink> Actions, string Id)
        {
            this.HtmlElement = new ContextMenu.Menu(htmlHelper,Actions,Id);
        }




    }
}
