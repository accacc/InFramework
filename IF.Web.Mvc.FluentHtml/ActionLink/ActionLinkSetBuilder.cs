using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.SubmitButton;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace IF.Web.Mvc.FluentHtml.Link
{
    public class ActionLinkSetBuilder : HtmlElementBuilder<ActionLinkSet, ActionLinkSetBuilder>
    {
        internal ActionLinkSet ActionSet;
        public ActionLinkSetBuilder(IHtmlHelper htmlhelper, List<AjaxLinkBuilder> actionSet,string gridViewId)
        {
            this.ActionSet = new ActionLinkSet(htmlhelper, actionSet, gridViewId);
            this.HtmlElement = this.ActionSet;
        }

        public ActionLinkSetBuilder Actions(Action<ActionLinkSpecialFactory> configurator)
        {
            ActionLinkSpecialFactory factory = new ActionLinkSpecialFactory(this.HtmlElement);
            configurator(factory);
            return this as ActionLinkSetBuilder;
        }
    }
}
