using Derin.Core.Data;
using Derin.Core.Handler;
using Derin.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Derin.Core.Data
{
    public class PagedListResponse<T> : BaseResponse, IPagedListResponse<T>
    {
        public  List<T> Data { get; private set; }
        public PagedListResponse()
        {
            this.Data = new List<T>();
          
        }

        public  PagedListResponse(IQueryable<T> source, BasePagingRequest request, Expression<Func<T, T>> columns=null)
        {
            this.Data = new List<T>();
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / request.PageSize;

            if (total % request.PageSize > 0)
                TotalPages++;

            this.PageSize = request.PageSize;
            this.PageNumber = request.PageNumber;

            if(request.Orders.Any())
            {
                source = source.Sort(request.Orders);
            }

            if(columns!=null)
            {
                this.Data.AddRange(source.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).Select<T, T>(columns).ToList());
            }
            else
            {

                this.Data.AddRange(source.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList());

            }


        }

        public PagedListResponse(IQueryable<T> source, int pageNumber, int pageSize, Expression<Func<T, T>> columns)
        {
            this.Data = new List<T>();
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data.AddRange(source.Skip((pageNumber-1) * pageSize).Take(pageSize).Select<T,T>(columns).ToList());
        }

        public void Initailize(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data.AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());

        }

       

        

        public PagedListResponse(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) { pageSize = 10; }
            if (pageNumber <= 0) { pageNumber = 1; }

            int total = source.Count();
            this.TotalCount = total;            

            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data.AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        private PagedListResponse(IEnumerable<T> source, int pageNumber, int pageSize, int totalCount)
        {

            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data.AddRange(source);
        }

        private PagedListResponse(IList<T> source, int pageNumber, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data.AddRange(source.Skip(pageNumber * pageSize).Take(pageSize).ToList());
        }

      

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageNumber > 0); }
        }
        public bool HasNextPage
        {
            get { return (PageNumber + 1 < TotalPages); }
        }

    }

    public interface IPagedList<T> : IList<T>
    {

        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        void Initailize(IQueryable<T> source, int pageIndex, int pageSize);
    }

    public class PagedList<T> : List<T>, IPagedList<T>
    {

        public PagedList()
        {

        }

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, Expression<Func<T, T>> columns)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select<T, T>(columns).ToList());
        }

        public void Initailize(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());

        }

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageSize <= 0) { pageSize = 10; }
            if (pageIndex <= 0) { pageIndex = 1; }

            int total = source.Count();
            this.TotalCount = total;

            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        private PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {

            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source);
        }

        private PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }



        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

    }


}
