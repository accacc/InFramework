//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;

//namespace Derin.Core.Mvc.Extensions
//{
//    public static class RadioButtonExtensions
//    {
//        public static MvcHtmlString EnumRadioButtonList<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, TEnum selectedValue)
//        {
//            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            
//            List<SelectListItem> selectList = DropDownListExtensions.GetSelectListFromEnum<TEnum>(null, selectedValue).ToList();

//            var sb = new StringBuilder();

//            if (selectList != null)
//            {
//                sb = sb.AppendFormat("<ul>");


//                foreach (var item in selectList)
//                {
//                    var label = "<label class='button-radio-label'>" + item.Text + "</label>"; 
//                    var radio = String.Empty;

//                    if (item.Selected)
//                    {
//                        radio = htmlHelper.RadioButtonFor(expression, item.Value, new { Checked = "checked" , @class="button-radio" }).ToHtmlString();
//                    }
//                    else
//                    {
//                        radio = htmlHelper.RadioButtonFor(expression, item.Value, new { @class = "button-radio" }).ToHtmlString();
//                    }

//                    sb.AppendFormat("<li>{0}{1}</li>", radio, HttpUtility.HtmlEncode(label));
//                }

//                sb = sb.AppendFormat("</ul>");
//            }

//            return MvcHtmlString.Create(sb.ToString());
//        }

        

//    }
//}
