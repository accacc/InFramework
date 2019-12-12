using IF.Core.Data;
using IF.Core.Log;
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
    public class MongoAuditLogRepository :MongoDbGenericRepository, IMongoAuditLogRepository
    {

        public MongoAuditLogRepository(IMongoDbConnectionStrategy connectionStrategy) : base(connectionStrategy)
        {
            
        }

        public async Task<AuditLog> GetDetailAsync(Guid uniqueId)
        {
            

                var fields = Builders<AuditLogMongoDb>.Projection.Include(e => e.JsonObject).Include(e=>e.ObjectName).Exclude("_id");

                var query = this.GetQuery<AuditLogMongoDb>(nameof(AuditLog)).Find(log => log.UniqueId == uniqueId).Project<AuditLog>(fields);

                var detail =  await query.SingleOrDefaultAsync();

                return detail;
            
        }

        public async Task<PagedListResponse<AuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source,string UserId ,int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<AuditLogMongoDb>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.LogDate, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.LogDate, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(Source))
            {
                var userFilter = filterBuilder.Eq(i => i.ObjectName, Source);
                filter = filter & userFilter;
            }

            if (!String.IsNullOrWhiteSpace(UserId))
            {
                var userFilter = filterBuilder.Eq(i => i.UserId, UserId);
                filter = filter & userFilter;
            }

            var fields = Builders<AuditLogMongoDb>.Projection.Exclude(e => e.JsonObject).Exclude("_id");

            var list = await this.GetQuery<AuditLogMongoDb>(nameof(AuditLog)).Find(filter).Project<AuditLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.LogDate).ToListAsync();


            var count = await this.GetQuery<AuditLogMongoDb>(nameof(AuditLog)).CountDocumentsAsync(filter);


            return new PagedListResponse<AuditLog>(list, PageNumber, PageSize, count);
        }

       
    }
}
