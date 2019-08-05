using IF.Core.Data;

namespace IF.Core.Handler
{
    public interface IDbPagingQuery<T>
    {
        PagedListResponse<T> GetPageList(BasePagingRequest param);
    }
}
