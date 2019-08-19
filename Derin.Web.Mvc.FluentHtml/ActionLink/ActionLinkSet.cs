using IF.Web.Mvc.FluentHtml.Link;
using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.SubmitButton
{
    public class ActionLinkSet: IF.Web.Mvc.FluentHtml.Base.HtmlElement, IActionLinkableElement
    {
        List<AjaxLinkBuilder> permissionedActions;
        string gridViewId;
        public ActionLinkSet(HtmlHelper htmlHelper,List<AjaxLinkBuilder> permissionedActions, string gridViewId):base(htmlHelper)
        {
            this.permissionedActions = permissionedActions;
            this.gridViewId = gridViewId;
            this.ActionLinks = new List<AjaxLinkBuilder>();
        }

        public IList<AjaxLinkBuilder> ActionLinks { get; set; }

        public override MvcHtmlString CreateHtml()
        {
            string links = String.Empty;

            foreach (var action in permissionedActions)
            {               
                links = links + action.Render().ToString() + "&nbsp";
            }

            foreach (var action in ActionLinks)
            {
                action.AjaxOptions(ajax => ajax.GridViewId(gridViewId));
                links = links + action.Render().ToString() + "&nbsp";

            }

            this.Builder.InnerHtml = links;

            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }

        public override string GetTag()
        {
            return ("div");
        }
    }
}
