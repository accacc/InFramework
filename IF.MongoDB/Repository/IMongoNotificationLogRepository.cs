using IF.Core.Data;
using IF.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoNotificationLogRepository
    {
        Task<IEnumerable<NotificationLog>> GetAllLogsAsync();
        Task<NotificationLog> GetLogsAsync(Guid id);

        Task<IEnumerable<NotificationLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        Task AddLogAsync(NotificationLog item);

        Task<PagedListResponse<NotificationLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string logger, int skipNumber = 0, int takeNumber = 50);




    }
}
