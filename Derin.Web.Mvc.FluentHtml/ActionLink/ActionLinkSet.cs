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


                //var link = new ActionLink.ActionLink(this.htmlHelper, button.Name,
                //               button.ActionName,
                //               button.ControllerName);
                //link.HtmlAttributes.Add("actionTypeId", button.actionTypeId);
                //link.HtmlAttributes.Add("class", "btn btn-primary btn-sm margin-bottom-20");
                //link.HtmlAttributes.Add("dataDialogId", Guid.NewGuid());
                //link.HtmlAttributes.Add("dataDialogRouteValue", button.dataDialogRouteValue);
                //link.HtmlAttributes.Add("dataDialogTitle", button.Name);
                //link.HtmlAttributes.Add("gridViewId", gridViewId);
                //link.HtmlAttributes.Add("PermissionMapId", button.PermissionMapId);

                //link.IconClassName = "glyphicon glyphicon-plus";


                //link.RouteValues = new { PermissionMapId = button.PermissionMapId };

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
