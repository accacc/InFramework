using IF.Core.Data;
using System.Threading.Tasks;

namespace IF.Core.Interfaces
{
    public interface IDbPagingQueryAsync<T>
    {
        Task<PagedListResponse<T>> GetPageListAsync(BasePagingRequest param);
    }
}
