using IF.Core.Data;
using System.Threading.Tasks;

namespace IF.Core.Handler
{
    public interface IDbPagingQueryAsync<T>
    {
        Task<PagedListResponse<T>> GetPageListAsync(BasePagingRequest param);
    }
}
