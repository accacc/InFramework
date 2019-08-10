using IF.Core.Data;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface IDataPagingQueryAsync<T>
    {
        Task<PagedListResponse<T>> GetPageListAsync(BasePagingRequest param);
    }
}
