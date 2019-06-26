using IF.Core.Data;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IF.Web.Mvc.FluentHtml.Pagination
{
    public class PaginationBuilder<T> where T : class
    {
        private readonly IHtmlHelper htmlHelper;
        private readonly PaginationAjax<T> paginationAjax;

        public PaginationBuilder(IHtmlHelper htmlHelper, IPagedListResponse<T> pagedList)
        {
            this.htmlHelper = htmlHelper;
            this.paginationAjax = new PaginationAjax<T>(pagedList);
            this.paginationAjax.ShowTotal = true;
        }



        public PaginationBuilder<T> MergeFormData(bool MergeFormData)
        {
            this.paginationAjax.MergeFormData = MergeFormData;
            return this;
        }

        public PaginationBuilder<T> Url(string url)
        {
            this.paginationAjax.Url = url;
            return this;
        }

        public PaginationBuilder<T> UpdateId(string updateId)
        {
            this.paginationAjax.UpdateId = updateId;
            return this;
        }



        public HtmlString Render()
        {
            return this.paginationAjax.Render();


        }

    }
}
