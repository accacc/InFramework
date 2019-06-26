using IF.Core.Data;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.Pagination
{
    public class PaginationAjax<T> where T : class
    {
        IPagedListResponse<T> pagedList;

        public PaginationAjax(IPagedListResponse<T> pagedList)
        {
            this.pagedList = pagedList;
        }

        public string Url { get; set; }

        public bool MergeFormData { get; set; }

        public string UpdateId { get; set; }

        public bool ShowTotal { get; set; }


        public HtmlString Render()
        {



            var div = new TagBuilder("div");

            if (ShowTotal)
            {
                div.InnerHtml.AppendHtmlLine("Toplam : " + pagedList.TotalCount + "<br>");
            }


            var ul = new TagBuilder("ul");
            ul.TagRenderMode = TagRenderMode.Normal;

            ul.AddCssClass("pagination justify-content-center");
            int? page = pagedList.PageNumber;
            var currentPage = page != null ? (int)page : 1;
            double startPage = currentPage - 5;
            double endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > pagedList.TotalPages)
            {
                endPage = pagedList.TotalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            if (endPage > 1)
            {

                if (currentPage > 1)

                {


                    var prevLi = new TagBuilder("li");
                    prevLi.TagRenderMode = TagRenderMode.Normal;

                    var prevLink = new TagBuilder("a");
                    prevLink.TagRenderMode = TagRenderMode.Normal;
                    prevLink.AddCssClass("page-link");
                    prevLink.MergeAttribute("id", "if-paging-number-link");

                    if (!string.IsNullOrWhiteSpace(this.Url))
                    {
                        prevLink.MergeAttribute("if-paging-url", this.Url);
                    }

                    prevLink.MergeAttribute("if-paging-update-id", this.UpdateId);
                    prevLink.MergeAttribute("if-paging-merge-formdata", this.MergeFormData.ToString());
                    prevLink.MergeAttribute("if-paging-pagenumber", (currentPage - 1).ToString());
                    prevLink.InnerHtml.Append("Önceki");



                    prevLi.InnerHtml.AppendHtml(prevLink);

                    ul.InnerHtml.AppendHtml(prevLi);
                }

                for (var i = startPage; i <= endPage; i++)
                {
                    var li = new TagBuilder("li");
                    li.TagRenderMode = TagRenderMode.Normal;

                    var pageLink = new TagBuilder("a");
                    pageLink.TagRenderMode = TagRenderMode.Normal;
                    pageLink.AddCssClass("page-link");
                    pageLink.MergeAttribute("id", "if-paging-number-link");

                    if (!string.IsNullOrWhiteSpace(this.Url))
                    {
                        pageLink.MergeAttribute("if-paging-url", this.Url);
                    }

                    pageLink.MergeAttribute("if-paging-update-id", this.UpdateId);
                    pageLink.MergeAttribute("if-paging-merge-formdata", this.MergeFormData.ToString());
                    pageLink.MergeAttribute("if-paging-pagenumber", i.ToString());
                    pageLink.InnerHtml.Append(i.ToString());

                    if (pagedList.PageNumber == i)
                    {
                        li.AddCssClass("page-item active");
                    }
                    else
                    {
                        li.AddCssClass("page-item");
                    }

                    li.InnerHtml.AppendHtml(pageLink);

                    ul.InnerHtml.AppendHtml(li);
                }

                if (currentPage < pagedList.TotalPages)

                {


                    var nextLi = new TagBuilder("li");
                    nextLi.TagRenderMode = TagRenderMode.Normal;

                    var nextLink = new TagBuilder("a");
                    nextLink.TagRenderMode = TagRenderMode.Normal;
                    nextLink.AddCssClass("page-link");
                    nextLink.MergeAttribute("id", "if-paging-number-link");

                    if (!string.IsNullOrWhiteSpace(this.Url))
                    {
                        nextLink.MergeAttribute("if-paging-url", this.Url);
                    }

                    nextLink.MergeAttribute("if-paging-update-id", this.UpdateId);
                    nextLink.MergeAttribute("if-paging-merge-formdata", this.MergeFormData.ToString());
                    nextLink.MergeAttribute("if-paging-pagenumber", (currentPage + 1).ToString());
                    nextLink.InnerHtml.Append("Sonraki");



                    nextLi.InnerHtml.AppendHtml(nextLink);

                    ul.InnerHtml.AppendHtml(nextLi);
                }

            }
            div.InnerHtml.AppendHtml(ul);

            var writer = new System.IO.StringWriter();
            div.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());


        }


    }
}
