using Derin.Core.Mvc.Model;
using IF.Core.Security;
using IF.Web.Mvc.FluentHtml.Grid;
using IF.Web.Mvc.FluentHtml.Link;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq;

namespace Derin.Web.Mvc.Kendo
{
    public static class KendoExtensions
    {


        //public static KendoForFactory<TModel> Kendo<TModel>(this IHtmlHelper<TModel> htmlHelper)
        //{

        //    return new KendoForFactory<TModel>(htmlHelper);
        //}


        //public static MvcHtmlString RenderWithFormGroup<TViewComponent, TBuilder>(this WidgetBuilderBase<TViewComponent, TBuilder> builder)
        //  where TViewComponent : WidgetBase
        //  where TBuilder : WidgetBuilderBase<TViewComponent, TBuilder>
        //{

        //    var component = builder.ToComponent();
        //    return GetFormControl(new HtmlHelper(component.ViewContext, new ViewPage()), component.ModelMetadata, component.Name, component);
        //}

        //private static MvcHtmlString GetFormControl(HtmlHelper htmlHelper, ModelMetadata metaData, string htmlFieldName, WidgetBase control)
        //{

        //    BootstrapFormGroup formGroup = new BootstrapFormGroup(htmlHelper);

        //    formGroup.Build();

        //    formGroup.AcceptChild(new IFLabel(htmlHelper, String.Empty, htmlFieldName, metaData));

        //    HtmlDivElement controlDiv = new HtmlDivElement(htmlHelper);
        //    controlDiv.Build();
        //    controlDiv.Builder.InnerHtml += MvcHtmlString.Create(control.ToHtmlString());

        //    formGroup.AcceptChild(controlDiv);

        //    formGroup.AcceptChild(new ValidationMessage(htmlHelper, htmlFieldName, metaData));

        //    return formGroup.Render();
        //}
        //public static GridBoundColumnBuilder<T> CheckboxColumnClient<T>(this GridColumnFactory<T> builder, string name = "checkedRecords") where T : BaseGridModel
        //{
        //    var column = builder.Bound(o => o.Id)
        //        .Title("")
        //        .Width(5)
        //        .HtmlAttributes(new { style = "text-align:center" })
        //        .HeaderTemplate(String.Format("<input type='checkbox' title='check all records' id='CheckAllOnGrid' data-checkbox-gridid='{0}' data-checkbox='checkall'", builder.Container.Name))
        //        .Width(50)
        //        .HeaderHtmlAttributes(new { style = "text-align:left" })
        //        .HtmlAttributes(new { style = "text-align:left" })
        //        .ClientTemplate(String.Format("<input type='checkbox' name='{0}' data-checkbox='checkone' value='#=Id#'/>", name))
        //        ;

        //    return column;
        //}




        public static GridBoundColumnBuilder<T> HighlightBoolean<T>(this GridBoundColumnBuilder<T> builder) where T : class
        {

            builder.HtmlAttributes(new { isbool = 1 });
            return builder;

        }

        public static GridBuilder<T> GridRowButtons<T>(this GridBuilder<T> builder, Action<ActionLinkSpecialFactory> configurator = null, ColumnPosition position = ColumnPosition.End)
  where T : BaseGridModel
        {

            //var securityContext = DependencyResolver.Current.GetService<ISecurityContext>();

            PermissionMapDto currentAction = new PermissionMapDto();//securityContext.CurrentAction();



            IHtmlHelper htmlHelper = builder.ToComponent().HtmlHelper;

            int positionIndex = 0;

            var gridComponent = builder.ToComponent();

            ActionLinkSetBuilder actionSetBuilder = GridHelper.GetGridRowButtons(currentAction, configurator, htmlHelper, builder.ToComponent().Name);

            if (position == ColumnPosition.End)
            {
                positionIndex = gridComponent.Columns.Count - 1;
                builder.Columns(c => c.Template(actionSetBuilder.Render().ToString())
                                  .Visible(true)
                                  
                                  .Title(String.Empty));
            }
            else
            {
                gridComponent.Columns.ElementAt(positionIndex).ClientTemplate = gridComponent.Columns.ElementAt(positionIndex).ClientTemplate + actionSetBuilder.Render().ToString();
            }

            return builder;
        }

//        public static GridBuilder<T> GridToolBarButtons<T>(this GridBuilder<T> builder, Action<ActionLinkSpecialFactory> configurator = null)
//where T : BaseGridModel
//        {


//            //var securityContext = DependencyResolver.Current.GetService<ISecurityContext>();

//            PermissionMapDto currentAction = new PermissionMapDto();//securityContext.CurrentAction();


//            HtmlHelper htmlHelper = new HtmlHelper(builder.ToComponent().ViewContext, new ViewPage());



//            ActionLinkSetBuilder actionSetBuilder = GridHelper.GetGridToolBarButtons(currentAction, configurator, htmlHelper, builder.ToComponent().Name);

//            builder.ToolBar(t => t.Template(actionSetBuilder.Render().ToString()));


//            return builder;


//        }
    }
}
