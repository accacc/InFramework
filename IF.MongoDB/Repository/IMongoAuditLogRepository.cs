using IF.Core.Data;
using IF.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllLogsAsync();
        Task<AuditLog> GetLogAsync(Guid id);

        Task<IEnumerable<AuditLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        Task AddLogAsync(AuditLog item);

        Task<AuditLog> GetDetailAsync(Guid uniqueId);

        Task<PagedListResponse<AuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId,int skipNumber = 0, int takeNumber = 50);
        void AddLog(AuditLog auditLog);
    }
}
