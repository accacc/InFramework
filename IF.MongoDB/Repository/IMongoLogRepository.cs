using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoLogRepository
    {
        Task<string> GetStackTraceAsync(Guid id);

        Task<ApplicationErrorLog> GetLogAsync(Guid id);

        Task<IEnumerable<ApplicationErrorLog>> GetAllLogsAsync();        

        Task<IEnumerable<ApplicationErrorLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        Task AddLogAsync(ApplicationErrorLog item);

        Task<PagedListResponse<ApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId,string Message, string Source, string Channel, int skipNumber = 0, int takeNumber = 50);




    }
}
