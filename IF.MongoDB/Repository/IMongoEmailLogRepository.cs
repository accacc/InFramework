using IF.Core.Data;
using IF.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoEmailLogRepository
    {
        Task<IEnumerable<EmailLog>> GetAllLogsAsync();
        Task<EmailLog> GetLogsAsync(Guid id);

        Task<IEnumerable<EmailLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        Task AddLogAsync(EmailLog item);

        Task<PagedListResponse<EmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int skipNumber = 0, int takeNumber = 50);

        Task<string> GetBodyAsync(Guid id);

    }
}
