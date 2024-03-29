﻿using IF.Core.Data;
using IF.Core.Email;
using IF.Core.MongoDb;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using IF.MongoDB.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{
    public class MongoEmailLogRepository :MongoDbGenericRepository,IMongoEmailLogRepository
    {
        

        //public MongoEmailLogRepository(MongoConnectionSettings settings) : base(settings)
        //{
            
        //}

        public MongoEmailLogRepository(IMongoDbConnectionStrategy connectionStrategy) : base(connectionStrategy)
        {

        }

        public async Task<PagedListResponse<IEmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<EmailLog>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.Date, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.Date, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(To))
            {
                var userFilter = filterBuilder.Eq(i => i.To, To);
                filter = filter & userFilter;
            }

            if (!String.IsNullOrWhiteSpace(type))
            {
                var loggerFilter = filterBuilder.Eq(i => i.Type, type);
                filter = filter & loggerFilter;
            }

            var fields = Builders<EmailLog>.Projection.Exclude(e => e.Body);


            var list = await this.GetQuery<EmailLog>().Find(filter).Project<IEmailLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await this.GetQuery<EmailLog>().CountDocumentsAsync(filter);

            


            return new PagedListResponse<IEmailLog>(list, PageNumber, PageSize, count);
        }

        public async Task<string> GetBodyAsync(Guid id)
        {
                var fields = Builders<EmailLog>.Projection.Include(e => e.Body);

                var body =  await this.GetQuery<EmailLog>()
                                .Find(log => log.UniqueId == id)
                                .Project<EmailLog>(fields)
                                .SingleOrDefaultAsync();

                return body.Body;
        }

    }
}
