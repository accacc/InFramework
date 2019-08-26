using IF.Core.Mvc;
using IF.Core.Security;
using IF.Web.Mvc.FluentHtml.Link;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IF.Web.Mvc.FluentHtml.Grid
{
    public static class GridHelper
    {
        public static ActionLinkSetBuilder GetGridButtons(PermissionMapDto currentAction, Action<ActionLinkSpecialFactory> configurator, IHtmlHelper htmlHelper, string gridViewId)
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
                AddLink(link);
                links.Add(link);
            }





            ActionLinkSetBuilder actionSetBuilder = new ActionLinkSetBuilder(htmlHelper, links, gridViewId);

            if (configurator != null)
            {
                ActionLinkSpecialFactory factory = new ActionLinkSpecialFactory(actionSetBuilder.HtmlElement);

                configurator(factory);

                foreach (var link in actionSetBuilder.HtmlElement.ActionLinks)
                {
                    
                    AddLink(link);
                }

            }

            return actionSetBuilder;
        }

        private static void AddLink(AjaxLinkBuilder link)
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

        //public static ActionLinkSetBuilder GetGridToolBarButtons(PermissionMapDto currentAction, Action<ActionLinkSpecialFactory> configurator, IHtmlHelper htmlHelper, string gridViewId)
        //{


        //    if (String.IsNullOrWhiteSpace(gridViewId))
        //    {
        //        throw new System.Exception("Lütfen gridin id'sini parametre olarak verin.");
        //    }

        //    List<PermissionMapDto> childActions = new List<PermissionMapDto>();


        //    if (currentAction != null && currentAction.Childs != null && currentAction.Childs.Any())
        //    {
        //        childActions = currentAction.Childs.Where(a => a.WidgetType == (int)ActionWidgetType.GridRowButton).ToList();
        //    }


        //    List<AjaxLinkBuilder> links = new List<AjaxLinkBuilder>();



        //    foreach (var childAction in childActions)
        //    {

        //        AjaxLinkBuilder link = new AjaxLinkBuilder(new ActionLink(htmlHelper, String.Empty, childAction.ActionName, childAction.ControllerName));

        //        link.HtmlElement.RouteValues.Add("PermissionMapId", childAction.Id);
        //        AddLink(link);
        //        links.Add(link);
        //    }





        //    ActionLinkSetBuilder actionSetBuilder = new ActionLinkSetBuilder(htmlHelper, links, gridViewId);

        //    if (configurator != null)
        //    {
        //        ActionLinkSpecialFactory factory = new ActionLinkSpecialFactory(actionSetBuilder.HtmlElement);

        //        configurator(factory);

        //        foreach (var link in actionSetBuilder.HtmlElement.ActionLinks)
        //        {

        //            AddLink(link);
        //        }

        //    }

        //    return actionSetBuilder;
        //}

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
