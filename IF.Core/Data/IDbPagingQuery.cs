using IF.Core.Data;

namespace IF.Core.Data
{
    public interface IDataPagingQuery<T>
    {
        PagedListResponse<T> GetPageList(BasePagingRequest param);
    }
}
