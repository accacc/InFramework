using Derin.Core.Handler;
using System.Collections.Generic;
using System.Linq;

namespace Derin.Core.Data
{

    public interface IPagedListResponse
    {
        int PageNumber { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
    public interface IPagedListResponse<T> : IPagedListResponse
    {

    }
}
