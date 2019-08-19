using IF.Core.Mvc;
using IF.Core.Security;
using IF.Web.Mvc.FluentHtml.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Grid
{
    public static class GridHelper
    {
        public static ActionLinkSetBuilder GetGridHeaderButtonsBuilder(PermissionMapDto currentAction, HtmlHelper htmlHelper, string gridViewId)
        {
            List<AjaxLinkBuilder> builder = new List<AjaxLinkBuilder>();

            List<PermissionMapDto> childActions = new List<PermissionMapDto>();

            if (String.IsNullOrWhiteSpace(gridViewId))
            {
                throw new System.Exception("Lütfen gridin id'sini parametre olarak verin.");
            }

            if (currentAction != null && currentAction.Childs != null && currentAction.Childs.Any())
            {
                childActions = currentAction.Childs.Where(a => a.WidgetType == (int)ActionWidgetType.GridButton).ToList();
            }
            else
            {
                return new ActionLinkSetBuilder(htmlHelper, builder, gridViewId);
            }


            foreach (var childAction in childActions)
            {
                builder.Add(new AjaxLinkBuilder(new IF.Web.Mvc.FluentHtml.Link.ActionLink(htmlHelper, childAction.Name, childAction.ActionName, childAction.ControllerName))
                    .AjaxOptions(ajax =>
                    {
                        //ajax.ActionTypeId(((ActionType)childAction.Type).ToString());
                        ajax.GridViewId(gridViewId);                        
                        ajax.UpdatedTargetId(Guid.NewGuid().ToString());
                    }
                    )
                    .CssClass("btn btn-primary btn-sm margin-bottom-20")
                    .RouteValues(childAction.RouteParameter ?? String.Empty)
                .Id(childAction.ActionName)) ;
            }

            return new ActionLinkSetBuilder(htmlHelper, builder, gridViewId);
        }


        public static ActionLinkSetBuilder GetGridRowButtons(PermissionMapDto currentAction, Action<ActionLinkSpecialFactory> configurator, HtmlHelper htmlHelper, string gridViewId)
        {


            if (String.IsNullOrWhiteSpace(gridViewId))
            {
                throw new System.Exception("Lütfen gridin id'sini parametre olarak verin.");
            }

            List<PermissionMapDto> childActions = new List<PermissionMapDto>();


            if (currentAction != null && currentAction.Childs != null && currentAction.Childs.Any())
            {
                childActions = currentAction.Childs.Where(a => a.WidgetType == (int)ActionWidgetType.GridRowButton).ToList();
            }


            List<AjaxLinkBuilder> links = new List<AjaxLinkBuilder>();


            foreach (var childAction in childActions)
            {

                AjaxLinkBuilder link = new AjaxLinkBuilder(new ActionLink(htmlHelper, String.Empty, childAction.ActionName, childAction.ControllerName));


                link.HtmlElement.RouteValues.Add("PermissionMapId", childAction.Id);

                SetRouteParameter(link, childAction.RouteParameter);

                string actionText = "Boş";

                string iconName = "question-sign";


                if (!String.IsNullOrWhiteSpace(childAction.IconName))
                {
                    iconName = childAction.IconName;
                }

                if (childAction.WidgetRenderType == (byte)ActionWidgetRenderType.Text)
                {
                    if (!String.IsNullOrWhiteSpace(childAction.Text))
                    {
                        actionText = childAction.Text;
                    }

                    link.Text(actionText);
                    link.CssClass("btn btn-primary btn-grid");

                }
                else if (childAction.WidgetRenderType == (byte)ActionWidgetRenderType.Icon)
                {
                    if (!String.IsNullOrWhiteSpace(childAction.IconName))
                    {
                        iconName = childAction.IconName;
                    }

                    link.IconClassName("fas fa-" + iconName);
                }



                link.AjaxOptions(ajax =>
                {
                    ajax.UpdatedTargetId(Guid.NewGuid().ToString());
                }).Id(childAction.ControllerName + childAction.ActionName)
                    .HtmlAttributes(new { title = childAction.Name })
                    .IconClassName("fas fa-" + iconName);

                links.Add(link);
            }





            ActionLinkSetBuilder actionSetBuilder = new ActionLinkSetBuilder(htmlHelper, links, gridViewId);

            if (configurator != null)
            {
                ActionLinkSpecialFactory factory = new ActionLinkSpecialFactory(actionSetBuilder.HtmlElement);

                configurator(factory);

                foreach (var link in actionSetBuilder.HtmlElement.ActionLinks)
                {
                    SetRouteParameter(link, link.HtmlElement.GridRouteColumns);

                    string iconClassName = "question-sign";

                    if (!String.IsNullOrWhiteSpace(link.HtmlElement.IconClassName))
                    {
                        iconClassName = link.HtmlElement.IconClassName;
                    }

                    string actionText = "Boş";

                    if (link.HtmlElement.RenderType == ActionWidgetRenderType.Text)
                    {
                        if (!String.IsNullOrWhiteSpace(link.HtmlElement.Text))
                        {
                            actionText = link.HtmlElement.Text;
                        }

                        link.Text(actionText);
                        link.CssClass("btn btn-primary btn-grid");

                    }
                    else if (link.HtmlElement.RenderType == ActionWidgetRenderType.Icon)
                    {
                        if (!String.IsNullOrWhiteSpace(link.HtmlElement.IconClassName))
                        {
                            iconClassName = link.HtmlElement.IconClassName;
                        }

                        link.IconClassName("fas fa-" + iconClassName);

                        link.Text(String.Empty);
                    }                   
                }

            }

            return actionSetBuilder;
        }

        public static ActionLinkSetBuilder GetGridToolBarButtons(PermissionMapDto currentAction, Action<ActionLinkSpecialFactory> configurator, HtmlHelper htmlHelper, string gridViewId)
        {


            if (String.IsNullOrWhiteSpace(gridViewId))
            {
                throw new System.Exception("Lütfen gridin id'sini parametre olarak verin.");
            }

            List<PermissionMapDto> childActions = new List<PermissionMapDto>();


            if (currentAction != null && currentAction.Childs != null && currentAction.Childs.Any())
            {
                childActions = currentAction.Childs.Where(a => a.WidgetType == (int)ActionWidgetType.GridRowButton).ToList();
            }


            List<AjaxLinkBuilder> links = new List<AjaxLinkBuilder>();



            foreach (var childAction in childActions)
            {

                AjaxLinkBuilder link = new AjaxLinkBuilder(new ActionLink(htmlHelper, String.Empty, childAction.ActionName, childAction.ControllerName));


                link.HtmlElement.RouteValues.Add("PermissionMapId", childAction.Id);


                string iconName = "question-sign";

                if (!String.IsNullOrWhiteSpace(childAction.IconName))
                {
                    iconName = childAction.IconName;
                }



                link.AjaxOptions(ajax=>{ ajax.UpdatedTargetId(Guid.NewGuid().ToString());ajax.GridViewId(gridViewId); })
                    
                    //.ActionTypeId(((ActionType)childAction.Type).ToString())
                    .Id(childAction.ControllerName + childAction.ActionName)
                    
                    .HtmlAttributes(new { title = childAction.Name })
                    .IconClassName("fas fa-" + iconName);
                links.Add(link);
            }





            ActionLinkSetBuilder actionSetBuilder = new ActionLinkSetBuilder(htmlHelper, links, gridViewId);

            if (configurator != null)
            {
                ActionLinkSpecialFactory factory = new ActionLinkSpecialFactory(actionSetBuilder.HtmlElement);

                configurator(factory);              

            }

            return actionSetBuilder;
        }

        private static void SetRouteParameter(AjaxLinkBuilder link, string routeParameter)
        {
            if (!String.IsNullOrWhiteSpace(routeParameter))
            {
                string[] @params = routeParameter.Split(',');

                foreach (var param in @params)
                {

                    link.HtmlElement.RouteValues.Add(param, String.Format("#= {0} #", param));
                }
            }


            link.HtmlElement.RouteValues.Add("Id", "#= Id #");
        }
    }

    public enum ColumnPosition
    {
        End = 0,
        Start = 1
    }
}
