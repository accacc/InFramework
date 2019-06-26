using IF.Core.Data;
using IF.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{
    public interface IMongoSmsLogRepository
    {
        Task<IEnumerable<SmsLog>> GetAllLogsAsync();
        Task<SmsLog> GetLogsAsync(Guid id);

        Task<IEnumerable<SmsLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        Task AddLogAsync(SmsLog item);

        Task<PagedListResponse<SmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int skipNumber = 0, int takeNumber = 50);
    }
}
