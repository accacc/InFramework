using IF.Core.Data;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{

    public class MongoSmsLogRepository : GenericRepository,IMongoSmsLogRepository
    {
        

        public MongoSmsLogRepository(string url, string db):base(url, db)
        {
            
        }

        

        public async Task<SmsLog> GetLogsAsync(Guid id)
        {
            
                return await this.GetQuery<SmsLog>()
                                .Find(log => log.UniqueId == id)
                                .SingleOrDefaultAsync();
            
        }

        //
        public async Task<IEnumerable<SmsLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {

            var query = this.GetQuery<SmsLog>().Find(log => log.Message.Contains(bodyText) &&
                                   log.Date >= updatedFrom).SortBy(s => s.Date);

            return await query.ToListAsync();

        }


   

        public async Task<PagedListResponse<SmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<SmsLog>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.Date, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.Date, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(number))
            {
                var userFilter = filterBuilder.Eq(i => i.Number, number);
                filter = filter & userFilter;
            }

         



            var list = await this.GetQuery<SmsLog>().Find(filter).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await this.GetQuery<SmsLog>().CountDocumentsAsync(filter);


            return new PagedListResponse<SmsLog>(list, PageNumber, PageSize, count);
        }

    }
}
