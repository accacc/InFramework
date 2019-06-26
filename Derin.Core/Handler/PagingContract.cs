using Derin.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
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
