using IF.Core.Data;
using IF.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.Persistence.EF.Core
{
    public static class PagedListResponseEFAsyncExtension
    {
        public static async Task<PagedListResponse<T>> ToPagedListResponseAsync<T>(this IQueryable<T> source, BasePagingRequest request)
        {
            

            if (request.PageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", request.PageNumber, "PageNumber cannot be below 1.");

            if (request.PageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", request.PageSize, "PageSize cannot be less than 1.");



            var count  = await source.CountAsync();



            var data = await   source.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            PagedListResponse<T> pagedList = new PagedListResponse<T>(data, request.PageNumber, request.PageSize,count);

            return pagedList;


        }
    }
}
