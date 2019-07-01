﻿using IF.Core.Data;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoNotificationLogRepository :GenericRepository,IMongoNotificationLogRepository
    {
        

        public MongoNotificationLogRepository(string url, string db):base(url, db)
        {
            
        }

        public async Task<IEnumerable<NotificationLog>> GetAllLogsAsync()
        {
            return await this.GetQuery<NotificationLog>().Find(_ => true).ToListAsync();

        }

        public async Task<NotificationLog> GetLogsAsync(Guid id)
        {
        
                return await this.GetQuery<NotificationLog>()
                                .Find(log => log.UniqueId == id)
                                .SingleOrDefaultAsync();
        
        }

        //
        public async Task<IEnumerable<NotificationLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            
                var query = this.GetQuery<NotificationLog>().Find(log => log.Response.Contains(bodyText) &&
                                       log.Date >= updatedFrom).SortBy(s => s.Date); 

                return await query.ToListAsync();
            
        }


        public async Task AddLogAsync(NotificationLog item)
        {
          
                await this.GetQuery<NotificationLog>().InsertOneAsync(item);
          
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




            var list = await this.GetQuery<NotificationLog>().Find(filter).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await this.GetQuery<NotificationLog>().CountDocumentsAsync(filter);


            return new PagedListResponse<NotificationLog>(list, PageNumber, PageSize, count);
        }

    }
}
