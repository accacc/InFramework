using IF.Core.Data;
using IF.Core.Log;
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
    public class MongoAuditLogRepository :GenericRepository, IMongoAuditLogRepository
    {

        public MongoAuditLogRepository(string url, string db):base(url, db)
        {
            
        }

       
        


       

        public async Task<IAuditLog> GetDetailAsync(Guid uniqueId)
        {
            

                var fields = Builders<AuditLog>.Projection.Include(e => e.JsonObject).Include(e=>e.ObjectName);

                var query = this.GetQuery<AuditLog>().Find(log => log.UniqueId == uniqueId).Project<AuditLog>(fields);

                var detail =  await query.SingleOrDefaultAsync();

                return detail;
            
        }

        public async Task<PagedListResponse<IAuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source,string UserId ,int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<AuditLog>.Filter;
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

            var fields = Builders<AuditLog>.Projection.Exclude(e => e.JsonObject);

            var list = await this.GetQuery<AuditLog>().Find(filter).Project<IAuditLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.LogDate).ToListAsync();


            var count = await this.GetQuery<AuditLog>().CountDocumentsAsync(filter);


            return new PagedListResponse<IAuditLog>(list, PageNumber, PageSize, count);
        }

       
    }
}
