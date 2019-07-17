using IF.Core.Data;
using IF.Core.Email;
using IF.Core.Log;
using IF.Core.Notification;
using IF.Core.Performance;
using IF.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoNotificationLogService : INotificationLogService
    {

        private readonly IMongoNotificationLogRepository repository;

        public MongoNotificationLogService(IMongoNotificationLogRepository repository)
        {
            this.repository = repository;
        }

        

        public async Task<PagedListResponse<INotificationLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string logger, int PageSize = 0, int PageNumber = 50)
        {
            return await this.repository.GetPaginatedAsync(BeginDate, EndDate, userId, logger, PageSize , PageNumber );
        }

        public async Task LogAsync(string DeviceId,bool Success, string Response, DateTime Date, Guid UniqueId,Guid SourceId)
        {
            NotificationLog log = new NotificationLog();
            log.Date = Date;
            log.Device = DeviceId;
            log.UniqueId = UniqueId;
            log.Response = Response;
            log.Success = Success;
            log.SourceId = SourceId;

            await this.repository.AddAsync(log);
            
        }
    }
}
