using IF.Web.Mvc.FluentHtml.CheckBox;
using IF.Web.Mvc.FluentHtml.DropDownList;
using IF.Web.Mvc.FluentHtml.Label;
using IF.Web.Mvc.FluentHtml.TextBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml
{
    public class HtmlElementForFactory<TModel> : HtmlElementFactory where TModel : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new HtmlHelper<TModel> HtmlHelper
        {
            get;
            set;
        }

        public HtmlElementForFactory(HtmlHelper<TModel> htmlHelper)
            : base(htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }


        public LabelBuilder LabelFor<TValue>(Expression<Func<TModel, TValue>> expression, string labelText = "")
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, this.HtmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var builder = new LabelBuilder(this.HtmlHelper, labelText, htmlFieldName, metaData);
            return builder;
        }

        public TextBoxBuilder TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, this.HtmlHelper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var value = String.Empty;

            if (metaData.Model != null)
            {
                value = metaData.Model.ToString();
            }

            var builder = new TextBoxBuilder(this.HtmlHelper, htmlFieldName, value, metaData);


            builder.IncludeValidationMessage(true);

            builder.Label(l => l.Add()).ValidationMessage(v => v.Add());



            return builder;
        }

        public CheckBoxBuilder CheckBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, this.HtmlHelper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var value = String.Empty;

            if (metaData.Model != null)
            {
                value = metaData.Model.ToString();
            }

            var builder = new CheckBoxBuilder(this.HtmlHelper, htmlFieldName, value, metaData);

            builder.IncludeValidationMessage(true);

            builder.Label(l => l.Add()).ValidationMessage(v => v.Add());

            return builder;
        }



        public DropDownListBuilder DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabelName = null)
        {
            var builder = GetDropDownListBuilder<TProperty>(expression, selectList, optionLabelName);


            return builder;
        }

        public DropDownListBuilder GetDropDownListBuilder<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabelName = null)
        {

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, this.HtmlHelper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var builder = new DropDownListBuilder(this.HtmlHelper, htmlFieldName, selectList, metaData);

            builder.CssClass("select2_category form-control");

            builder.IncludeValidationMessage(true);

            if (!string.IsNullOrEmpty(optionLabelName))
            {
                builder.optionLabel(optionLabelName);
            }

            builder.Label(l => l.Add()).ValidationMessage(v => v.Add());

            return builder;
        }




    }
}
