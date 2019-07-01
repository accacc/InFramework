using IF.Core.Data;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoEmailLogRepository:IRepository
    {
        
        Task<IEnumerable<EmailLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        
        Task<PagedListResponse<EmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int skipNumber = 0, int takeNumber = 50);

        Task<string> GetBodyAsync(Guid id);

    }
}
