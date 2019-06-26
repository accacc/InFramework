using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.DynamicData
{
    public static class DynamicDataExtensions
    {
       

        public static DynamicDataResponse<T> ToDynamicDataResponse<T>(this IQueryable<T> query,DynamicDataRequest request,  Expression<Func<T, T>> columns = null)
        {
            // call another method here to get filtering and sorting.

            var filtering = request.GetFiltering<T>();
            var sorting = request.GetSorting();

            var tempQuery = query
                .Where(filtering)
                .OrderBy(sorting);

            var Total = tempQuery
                .Count();

            var Data = tempQuery
                .Skip(request.Skip)
                .Take(request.Take).Select<T, T>(columns);

            return new DynamicDataResponse<T>()
            {
                Data = Data,
                Total = Total
            };
        }

        public static DynamicDataResponse<T> ToDynamicDataResponse<T>(this IEnumerable<T> list,DynamicDataRequest request)
        {
            var filtering = request.GetFiltering<T>();
            var sorting = request.GetSorting();

            var  tempQuery = list.AsQueryable()
                .Where(filtering)
                .OrderBy(sorting).ToList();

            var Total = tempQuery.Count();


            var Data = tempQuery
                .Skip(request.Skip)
                .Take(request.Take);

            return new DynamicDataResponse<T>()
            {
                Data = Data,
                Total = Total
            };
        }


        public static DynamicDataResponse<T> ToDynamicDataResponse<T>(this IEnumerable<T> list, int totalCount)
        {
            // Just use the request as a container
            var Data = list;
            var Total = totalCount;

            return new DynamicDataResponse<T>()
            {
                Data = Data,
                Total = Total
            };
        }


    }
}
