using IF.Web.Mvc.FluentHtml.DropDownList;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Extension
{
    public class HtmlElementForFactory<TModel> 
    {

        [RazorInject]
        public IModelExpressionProvider ModelExpressionProvider { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IHtmlHelper<TModel> HtmlHelper
        {
            get;
            set;
        }

        public HtmlElementForFactory(IHtmlHelper<TModel> htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

    }
}
