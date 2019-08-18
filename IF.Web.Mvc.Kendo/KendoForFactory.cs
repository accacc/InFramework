using Derin.Core.Mvc.Model;
using IF.Web.Mvc.FluentHtml.Extension;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Derin.Web.Mvc.Kendo
{
    public class KendoForFactory<TModel> where TModel : class
    {

        [RazorInject]
        public IModelExpressionProvider ModelExpressionProvider { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IHtmlHelper<TModel> HtmlHelper
        {
            get;
            set;
        }
        public KendoForFactory(HtmlHelper<TModel> htmlHelper)
            
        {
            this.HtmlHelper = htmlHelper;
        }



        public MultiSelectBuilder MultiSelectFor<TProperty>(Expression<System.Func<TModel, TProperty>> expression,string controllerName,string actionName,string placeHolder)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().MultiSelectFor<TProperty>(expression);
            builder.DataSource(source =>
             {
                 source.Read(actionName,controllerName);
                 source.ServerFiltering(true);


             });

            builder.Placeholder(placeHolder);
            builder.AutoClose(true);
            builder.MinLength(3);
            builder.DataTextField("Name");
            builder.DataValueField("Value");
            builder.AutoBind(false);
            builder.HtmlAttributes(new { @class = "form-control", @style = "width: 100%" });
            return builder;

        }

        public NumericTextBoxBuilder<TValue> IntegerTextBox<TValue>(Expression<System.Func<TModel, TValue>> expression) where TValue : struct
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<TValue>(expression);
            builder.Decimals(0);
            //builder.HtmlAttributes(new { @class = "form-control" });
            return builder;

        }


        public NumericTextBoxBuilder<TValue> IntegerTextBox<TValue>(Expression<System.Func<TModel, TValue?>> expression) where TValue : struct
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<TValue>(expression);
            builder.Decimals(0);
            //builder.HtmlAttributes(new { @class = "form-control" });
            return builder;

        }

        public NumericTextBoxBuilder<decimal> MoneyTextBox(Expression<System.Func<TModel, decimal>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<decimal>(expression);
            //builder.Format("c");
            //builder.HtmlAttributes(new { @class = "form-control" });
            return builder;

        }

        public NumericTextBoxBuilder<decimal> MoneyTextBox(Expression<System.Func<TModel, decimal?>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<decimal>(expression);
            //builder.Format("c");
            //builder.HtmlAttributes(new { @class = "form-control" });
            return builder;

        }

        public NumericTextBoxBuilder<decimal> DecimalTextBox(Expression<System.Func<TModel, decimal>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<decimal>(expression);
            return builder;
        }

        public NumericTextBoxBuilder<decimal> DecimalTextBox(Expression<System.Func<TModel, decimal?>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<decimal>(expression);
            return builder;
        }



        public NumericTextBoxBuilder<float> FloatTextBox(Expression<System.Func<TModel, float>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().NumericTextBoxFor<float>(expression);
            return builder;
        }

        public MaskedTextBoxBuilder PhoneMaskedTextBox<TProperty>(Expression<System.Func<TModel, TProperty>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().MaskedTextBoxFor<TProperty>(expression);
            builder.HtmlAttributes(new { @class = "form-control", data_masked_type = "phone" });
            builder.UnmaskOnPost(true);
            builder.Mask("000 000 0000");

            return builder;

        }

        public MaskedTextBoxBuilder CreditCardTextBox<TProperty>(Expression<System.Func<TModel, TProperty>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().MaskedTextBoxFor<TProperty>(expression);
            builder.HtmlAttributes(new { @class = "form-control", @style = "width: 100%" });
            builder.Mask("0000 0000 0000 0000");
            builder.UnmaskOnPost(true);
            return builder;

        }


        public DatePickerBuilder DatePickerFor(Expression<System.Func<TModel, DateTime?>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().DatePickerFor(expression);
            //builder.HtmlAttributes(new { style = "width: 100%" });
            return builder;

        }

        //public MobileSwitchBuilder SwitchFor(Expression<System.Func<TModel, bool>> expression)
        //{
        //    ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, this.HtmlHelper.ViewData);

        //    string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

        //    var value = String.Empty;

        //    if (metaData.Model != null)
        //    {
        //        value = metaData.Model.ToString();
        //    }

        //    bool IsChecked = false;

        //    if (value == "true")
        //    {
        //        IsChecked = true;
        //    }

        //    var builder = this.HtmlHelper.Kendo<TModel>().MobileSwitch().Checked(IsChecked).Name(htmlFieldName);
        //    //builder.HtmlAttributes(new { style = "width: 100%" });
        //    return builder;

        //}

        public DatePickerBuilder DatePickerFor(Expression<System.Func<TModel, DateTime>> expression)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().DatePickerFor(expression).Format(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
            builder.HtmlAttributes(new { style = "width: 100%" });
            return builder;

        }

        public DatePickerBuilder DatePickerFor(Expression<System.Func<TModel, DateTime>> expression, string format = null)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().DatePickerFor(expression).Format(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
            //builder.HtmlAttributes(new { style = "width: 100%" });
            return builder;

        }

      
        //public MvcHtmlString RangeTextBoxFor(Expression<System.Func<TModel, int>> expression1, Expression<System.Func<TModel, int>> expression2)
        //{
           

        //    TagBuilder inputBox = new TagBuilder("div");

        //    inputBox.AddCssClass("input-range-box");

        //    TagBuilder formGroup1 = new TagBuilder("div");
        //    formGroup1.InnerHtml = this.HtmlHelper.Kendo<TModel>().IntegerTextBoxFor(expression1).ToHtmlString();
        //    formGroup1.AddCssClass("form-group range-box");

        //    TagBuilder seperator = new TagBuilder("div");
        //    seperator.AddCssClass("seperator");
        //    seperator.InnerHtml = "-";

        //    TagBuilder formGroup2 = new TagBuilder("div");
        //    formGroup2.InnerHtml = this.HtmlHelper.Kendo<TModel>().IntegerTextBoxFor(expression2).ToHtmlString();
        //    formGroup2.AddCssClass("form-group range-box");


        //    inputBox.InnerHtml = formGroup1.ToString(TagRenderMode.Normal) + 
        //        seperator.ToString(TagRenderMode.Normal) +
        //        formGroup2.ToString(TagRenderMode.Normal);


        //    return MvcHtmlString.Create(inputBox.ToString(TagRenderMode.Normal));


        //}

        // <div class="filter-box range-wrap">
        //    <h4>Tahmini Stok Finans Giderleri</h4>
        //    <div class="input-range-box">
        //        <div class="form-group range-box">
        //            @(Html.Kendo().IntegerTextBoxFor(m => m.OtherFeatures.StockFinancingMinPrice))
        //        </div>
        //        <div class="seperator">-</div>
        //        <div class="form-group range-box">
        //            @(Html.Kendo().IntegerTextBoxFor(m => m.OtherFeatures.StockFinancingMaxPrice))
        //        </div>
        //    </div>
        //</div>

        public DateTimePickerBuilder DateTimePickerFor(Expression<System.Func<TModel, DateTime>> expression, string dateTime = null)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().DateTimePickerFor(expression);
            builder.ToClientTemplate();
            if (dateTime != null)
                builder.Min(DateTime.Now.Date.ToString());
            return builder;

        }


        public TimePickerBuilder TimePickerFor(Expression<System.Func<TModel, TimeSpan>> expression, string dateTime = null)
        {
            var builder = this.HtmlHelper.Kendo<TModel>().TimePickerFor(expression);
            builder.ToClientTemplate();
            return builder;

        }

        //public DropDownListBuilder EnumDropDownList<TEnum>(Expression<System.Func<TModel, TEnum>> expression, TEnum selectedValue)
        //{
        //    var builder = this.HtmlHelper.Kendo<TModel>().DropDownListFor(expression);
        //    builder.HtmlAttributes(new { style = "width: 100%" });
            
        //    List<SelectListItem> selectList = DropDownListExtensions.GetSelectListFromEnum<TEnum>(null, selectedValue).ToList();
        //    var selectedItem = selectList.Where(s => s.Selected == true).SingleOrDefault();
        //    if (selectedItem != null)
        //    {
        //        var selectedIndex = selectList.IndexOf(selectedItem);
        //        builder.SelectedIndex(selectedIndex);
        //    }
            
        //    builder.BindTo(selectList);
        //    return builder;

        //}



        public GridBuilder<TModel> GridAjax(string actionName, string controllerName,string gridViewId, object routeValues = null)
        {

            return KendoHelper.GetBaseGrid(this.HtmlHelper, actionName, controllerName,gridViewId,routeValues);
        }



        public GridBuilder<T> GridServer<T>(IEnumerable<T> model, bool IsHiddenIdColumnEnabled = true) where T : BaseGridModel
        {
            var builder = this.HtmlHelper.Kendo().Grid<T>(model);



            if (IsHiddenIdColumnEnabled)
            {
                builder.Columns(c => c.Bound(k => k.Id).Hidden(true));
            }

            //builder.Scrollable();


            builder.Pageable(paging => paging.Enabled(true));

            builder.Selectable(select => select.Enabled(true));



            builder.Reorderable(reorder => reorder.Columns(true));

            return builder;
        }

    }
}
