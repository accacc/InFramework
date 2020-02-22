using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IF.Core.Data
{
    public class PagedListResponse<T> : BaseResponse, IPagedListResponse<T>
    {
        public  List<T> Data { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get;  set; }
        public double TotalCount { get;  set; }
        public double TotalPages { get;  set; }

        public bool HasPreviousPage
        {
            get { return (PageNumber > 1); }
        }
        public bool HasNextPage
        {
            get { return (PageNumber < TotalPages); }
        }
        public PagedListResponse()
        {
            this.Data = new List<T>();
          
        }

        public PagedListResponse(IQueryable<T> source, BasePagingRequest request, Expression<Func<T, T>> columns = null)
        {


            if (request.PageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", request.PageNumber, "PageNumber cannot be below 1.");

            if (request.PageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", request.PageSize, "PageSize cannot be less than 1.");

            
            
            this.TotalCount = source.Count();
            this.TotalPages = Math.Ceiling(TotalCount / request.PageSize);

            //if (total % request.PageSize > 0)
            //    TotalPages++;

            this.PageSize = request.PageSize;
            this.PageNumber = request.PageNumber;

            //if(request.Orders.Any())
            //{
            //    source = source.Sort(request.Orders);
            //}

            this.Data = new List<T>();

            if (columns != null)
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

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            
            
            this.TotalCount = source.Count();
            this.TotalPages = Math.Ceiling(TotalCount / pageSize);

            //if (total % pageSize > 0)
            //    TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = new List<T>();
            this.Data.AddRange(source.Skip((pageNumber-1) * pageSize).Take(pageSize).Select<T,T>(columns).ToList());
        }

        public void Initailize(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            
            this.TotalCount = source.Count();
            this.TotalPages = Math.Ceiling(TotalCount / pageSize);

            //if (total % pageSize > 0)
            //    TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = new List<T>();
            this.Data.AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());

        }

       

        

        public PagedListResponse(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) { pageSize = 10; }
            if (pageNumber <= 0) { pageNumber = 1; }

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            
            this.TotalCount = source.Count(); ;
            this.TotalPages = Math.Ceiling(TotalCount / pageSize);

            //if (total % pageSize > 0)
            //    TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = new List<T>();
            this.Data.AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public PagedListResponse(IList<T> source, int pageNumber, int pageSize, double totalCount)
        {

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            TotalCount = totalCount;
            this.TotalPages = Math.Ceiling(TotalCount / pageSize);

            //if (TotalCount % pageSize > 0)
            //    TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = new List<T>();
            this.Data.AddRange(source);
        }

        public PagedListResponse(IList<T> source, int pageNumber, int pageSize)
        {

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            TotalCount = source.Count();
            this.TotalPages = Math.Ceiling(TotalCount / pageSize);

            //if (TotalCount % pageSize > 0)
            //    TotalPages++;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = new List<T>();
            this.Data.AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

      

       

    }

   


}
