using IF.Core.Data;

namespace IF.Core.Interfaces
{
    public interface IDbPagingQuery<T>
    {
        PagedListResponse<T> GetPageList(BasePagingRequest param);
    }
}
