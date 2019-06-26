using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml
{
    public static class MvcHelper
    {

        internal static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            Debug.Assert(tagBuilder != null);
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }
        internal static object GetModelStateValue(this HtmlHelper helper, string key, Type destinationType)
        {
            ModelState modelState;
            if (helper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

        internal static string EvalString(this HtmlHelper helper, string key, string format)
        {
            return System.Convert.ToString(helper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        internal static IEnumerable<SelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object o = null;

            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(name);
            }

            if (o == null)
            {
                throw new InvalidOperationException("HtmlHelper_MissingSelectData " + name + " IEnumerable<SelectListItem>");
            }

            IEnumerable<SelectListItem> selectList = o as IEnumerable<SelectListItem>;

            if (selectList == null)
            {
                throw new InvalidOperationException("HtmlHelper_MissingSelectData " + name + " " + o.GetType().FullName + " IEnumerable<SelectListItem>");
            }

            return selectList;
        }


    }
}
