using IF.Core.Data;
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
    public class MongoEmailLogRepository :GenericRepository,IMongoEmailLogRepository
    {
        

        public MongoEmailLogRepository(string url, string db):base(url, db)
        {
            
        }

        

        
        //
        public async Task<IEnumerable<EmailLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
                var query = this.GetQuery<EmailLog>().Find(log => log.Body.Contains(bodyText) &&
                                       log.Date >= updatedFrom).SortBy(s => s.Date); 

                return await query.ToListAsync();
        }


       

        public async Task<PagedListResponse<EmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int PageNumber = 0, int PageSize = 50)

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


            var list = await this.GetQuery<EmailLog>().Find(filter).Project<EmailLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await this.GetQuery<EmailLog>().CountDocumentsAsync(filter);

            


            return new PagedListResponse<EmailLog>(list, PageNumber, PageSize, count);
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
