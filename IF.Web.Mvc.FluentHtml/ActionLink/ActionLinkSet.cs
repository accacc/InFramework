using IF.Web.Mvc.FluentHtml.Link;
using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IF.Web.Mvc.FluentHtml.SubmitButton
{
    public class ActionLinkSet: IF.Web.Mvc.FluentHtml.Base.HtmlElement, IActionLinkableElement
    {
        List<AjaxLinkBuilder> permissionedActions;
        string gridViewId;
        public ActionLinkSet(IHtmlHelper htmlHelper,List<AjaxLinkBuilder> permissionedActions, string gridViewId):base(htmlHelper)
        {
            this.permissionedActions = permissionedActions;
            this.gridViewId = gridViewId;
            this.ActionLinks = new List<AjaxLinkBuilder>();
        }

        public IList<AjaxLinkBuilder> ActionLinks { get; set; }

        public override HtmlString CreateHtml()
        {
            string links = String.Empty;

            foreach (var action in permissionedActions)
            {               

                links = links + action.Render().ToString() + "&nbsp";
            }

            foreach (var action in ActionLinks)
            {
                action.AjaxOptions(ajax => ajax.RefreshGridOnSuccess(gridViewId));
                links = links + action.Render().ToString() + "&nbsp";

            }

            this.Builder.InnerHtml.AppendHtml(new HtmlString(links));

            return this.Builder.Render();
        }

        public override string GetTag()
        {
            return ("div");
        }
    }
}
