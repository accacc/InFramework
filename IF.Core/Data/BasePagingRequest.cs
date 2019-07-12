using IF.Core.Data;
using System.Collections.Generic;

namespace IF.Core.Handler
{


    public class BasePagingRequest:BaseRequest,IPaginationInfo
    {
        public BasePagingRequest()
        {
            this.Orders = new List<Order>();
        }
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public List<Order> Orders { get; set; }
     
    }

    public class Order
    {
        /// <summary>
        /// Sorting column
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Sorting direction
        /// </summary>
        public bool IsDescending { get; set; }

        /// <summary>
        /// Priority of the orders
        /// </summary>
        public int Priority { get; set; }
    }

    //public class BasePagingResponse<T>
    //{
    //    public PagedList<T> PageList { get; private set; }


    //    BasePagingRequest request;
    //    Expression<Func<T, T>> columns;
    //    public BasePagingResponse(BasePagingRequest request)
    //    {
    //        this.request = request;            

    //    }



    //    public BasePagingResponse<T> Columns(Expression<Func<T, T>> columns)
    //    {
    //        this.columns = columns;
    //        return this;
    //    }

    //    public BasePagingResponse<T> ToPage(IQueryable<T> query)
    //    {
    //        if (columns != null)
    //        {
    //            this.PageList = new PagedList<T>(query, request.PageNumber, request.PageSize, columns);
    //        }
    //        else
    //        {
    //            this.PageList = new PagedList<T>(query, request.PageNumber, request.PageSize);
    //        }

    //        return this;
    //    }


    //}
}
