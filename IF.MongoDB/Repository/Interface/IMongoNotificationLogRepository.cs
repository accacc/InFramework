using IF.Core.Data;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoNotificationLogRepository:IRepository
    {
        
        Task<IEnumerable<NotificationLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        

        Task<PagedListResponse<NotificationLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string logger, int skipNumber = 0, int takeNumber = 50);




    }
}
