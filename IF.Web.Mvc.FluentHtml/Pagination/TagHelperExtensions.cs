using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.Core.Data;

namespace IF.Web.Mvc.FluentHtml.Pagination
{
    public static class TagHelperExtensions
    {
        public static PaginationBuilder<T> Paging<T>(this IHtmlHelper helper, IPagedListResponse<T> pagedListResponse) where T : class
        {

            return new PaginationBuilder<T>(helper, pagedListResponse);
        }

    }
}
