using IF.Core.Data;
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
        

        Task<IEnumerable<AuditLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);        

        Task<AuditLog> GetDetailAsync(Guid uniqueId);

        Task<PagedListResponse<AuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId,int skipNumber = 0, int takeNumber = 50);
        
    }
}
