using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Extension
{
    public static class HtmlElementFactoryExtension
    {
        public static HtmlElementFactory IF(this IHtmlHelper htmlHelper)
        {         

            return new HtmlElementFactory(htmlHelper);
        }

        public static HtmlElementForFactory<TModel> IFFor<TModel>(this IHtmlHelper<TModel> htmlHelper)
        {

            return new HtmlElementForFactory<TModel>(htmlHelper);
        }
    }
}
