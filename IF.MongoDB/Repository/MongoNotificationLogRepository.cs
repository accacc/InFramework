using IF.Core.Data;
using IF.MongoDB.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoNotificationLogRepository : IMongoNotificationLogRepository
    {
        private readonly LogContext _context = null;

        public MongoNotificationLogRepository(string url, string db)
        {
            _context = new LogContext(url, db);
        }

        public async Task<IEnumerable<NotificationLog>> GetAllLogsAsync()
        {
            try
            {
                return await _context.NotificationLogs.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NotificationLog> GetLogsAsync(Guid id)
        {
            try
            {
                return await _context.NotificationLogs
                                .Find(log => log.UniqueId == id)
                                .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public async Task<IEnumerable<NotificationLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.NotificationLogs.Find(log => log.Response.Contains(bodyText) &&
                                       log.Date >= updatedFrom).SortBy(s => s.Date); 

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task AddLogAsync(NotificationLog item)
        {
            try
            {
                await _context.NotificationLogs.InsertOneAsync(item);
            }
            catch // (Exception ex)
            {
                //throw ex;
            }
        }

        public async Task<PagedListResponse<NotificationLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string text, string deviceId, int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<NotificationLog>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.Date, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.Date, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(text))
            {
                var userFilter = filterBuilder.Text(text);
                filter = filter & userFilter;
            }

            if (!String.IsNullOrWhiteSpace(deviceId))
            {
                var loggerFilter = filterBuilder.Eq(i => i.Device, deviceId);
                filter = filter & loggerFilter;
            }




            var list = await _context.NotificationLogs.Find(filter).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await _context.NotificationLogs.CountDocumentsAsync(filter);


            return new PagedListResponse<NotificationLog>(list, PageNumber, PageSize, count);
        }

    }
}
