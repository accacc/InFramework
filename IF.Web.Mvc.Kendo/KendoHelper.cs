using Derin.Core.Mvc.Model;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;


namespace IF.Web.Mvc.Kendo
{
    public static class KendoHelper
    {
        public static GridBuilder<TModel> GetBaseGrid<TModel>(IHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, string gridViewId, object routeValues) where TModel : class
        {
            

            var builder = htmlHelper.Kendo().Grid<TModel>();

            //var securityContext = DependencyResolver.Current.GetService<ISecurityContext>();

            //var currentAction = securityContext.CurrentAction();           


            builder.Name(gridViewId);

            builder
              .Columns(
              c =>
              c.Bound("Id")
              .Visible(false)
              .ClientTemplate(String.Empty)
              .Title(String.Empty)
              .Filterable(false)
              );


            builder.DataSource(dataBinding =>
            {
                dataBinding.Ajax().PageSize(25).Read(read => read.Action(actionName, controllerName, routeValues)
                .Data("FilterGrid")
                );
            });


            builder.Pageable(settings => settings.
                                           PageSizes(new[] { 25, 50, 100, 200, 500, 1000 })
                                          .Refresh(true)
                                          .ButtonCount(20)
                                      );


            builder.Sortable();
            builder.EnableCustomBinding(true);
            builder.Selectable(select => select.Enabled(false));
            builder.Reorderable(reorder => reorder.Columns(true));
            builder.Scrollable(scrollable => scrollable.Height(540));
            builder.Filterable(f => f.Mode(GridFilterMode.Row).Extra(false));
            builder.NoRecords(x => x.Template("<div class='empty-grid'></div>"));
            return builder;
        }

        public static GridBuilder<T> HighlightBooleanRecord<T>(this GridBuilder<T> builder) where T : class
        {
            builder.Events(e => e.DataBound("HighlightBooleanRecord"));


            //if (grid.IsClientBinding)
            //{
            //    grid.ClientEvents.OnRowDataBound.HandlerName = "HighlightBooleanRecord";
            //}
            //else
            //{
            //    grid.RowAction = row =>
            //    {
            //        row. HtmlAttributes["class"] = "HighlightBooleanRecord";
            //    };
            //}

            return builder;

        }
    }
}
