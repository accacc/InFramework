using IF.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace IF.Core.Data
{

    public interface IPagedListResponse
    {
        int PageNumber { get; }
        int PageSize { get; }
        double TotalCount { get; }
        double TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
    public interface IPagedListResponse<T> : IPagedListResponse
    {
        List<T> Data { get; set; }
    }
}
