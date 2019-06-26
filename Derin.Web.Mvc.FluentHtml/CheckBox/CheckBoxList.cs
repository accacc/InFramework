//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Text;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace IF.Web.Mvc.FluentHtml.CheckBox
//{
//    public static class CheckBoxListHtmlHelper
//    {

//        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<String, Object> htmlAttributes) where TModel : class
//        {
//            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);


//            String field = ExpressionHelper.GetExpressionText(expression);


//            String name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(field);


//            return CheckBoxList(htmlHelper, metadata.DisplayName ?? metadata.PropertyName ?? field, selectList, htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes));


//        }
//        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, String name, IEnumerable<SelectListItem> selectList, IDictionary<String, Object> htmlAttributes)
//        {

//            TagBuilder list = new TagBuilder("ul");

//            list.AddCssClass("list-inline");
            
//            list.MergeAttributes<String, Object>(htmlAttributes);

//            StringBuilder items = new StringBuilder();

//            Int32 index = 1;

//            foreach (SelectListItem i in selectList)
//            {

//                TagBuilder input = new TagBuilder("input");
//                if (i.Selected) input.MergeAttribute("checked", "checked");
//                input.MergeAttribute("id", String.Concat(name, index));
//                input.MergeAttribute("name", name);
//                input.MergeAttribute("type", "checkbox");
//                input.MergeAttribute("data-checkbox", i.Value);
//                input.MergeAttribute("value", i.Value);
//                //class="make-switch" data-size="mini" data-on-color="warning" data-off-color="danger"
//                input.MergeAttribute("class", "make-switch");
//                input.MergeAttribute("data-size", "mini");
//                input.MergeAttribute("data-on-color", "warning");
//                input.MergeAttribute("data-off-color", "danger");
//                TagBuilder label = new TagBuilder("label");
//                label.MergeAttribute("for", String.Concat(name, index));
//                label.InnerHtml = i.Text;

//                items.AppendFormat("<li>{0}{1}</li>", input.ToString(TagRenderMode.Normal), label.ToString(TagRenderMode.Normal));

//                index++;

//            }

//            list.InnerHtml = items.ToString();

//            return MvcHtmlString.Create(list.ToString(TagRenderMode.Normal));

//        } // CheckBoxLis
//    }
//}
