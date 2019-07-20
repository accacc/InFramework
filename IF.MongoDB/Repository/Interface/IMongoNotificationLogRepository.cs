using IF.Core.Data;
using IF.Core.Notification;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoNotificationLogRepository:IMongoDbGenericRepository
    {
        

        

        Task<PagedListResponse<INotificationLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string logger, int PageNumber = 0, int PageSize = 50);




    }
}
