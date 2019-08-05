using IF.Core.Data;

namespace IF.Core.Data
{
    public interface IDbPagingQuery<T>
    {
        PagedListResponse<T> GetPageList(BasePagingRequest param);
    }
}
