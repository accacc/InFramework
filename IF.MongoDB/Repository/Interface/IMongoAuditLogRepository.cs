using IF.Core.Data;
using IF.Core.Log;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoAuditLogRepository:IRepository
    {
        

        Task<IEnumerable<IAuditLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);        

        Task<IAuditLog> GetDetailAsync(Guid uniqueId);

        Task<PagedListResponse<IAuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId,int skipNumber = 0, int takeNumber = 50);
        
    }
}
