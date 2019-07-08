using IF.Core.Data;
using IF.Core.Log;
using IF.MongoDB.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{


    


    public class MongoLogRepository : GenericRepository,IMongoLogRepository
    {

        public MongoLogRepository(string cnnString, string database):base(cnnString,database)
        {

        }

        public async Task<string> GetStackTraceAsync(Guid id)
        {

            try
            {
                var fields = Builders<ApplicationErrorLog>.Projection.Include(e => e.StackTrace);

                var log = await this.GetQuery<ApplicationErrorLog>()
                                .Find(e => e.UniqueId == id)
                                .Project<ApplicationErrorLog>(fields)
                                .SingleOrDefaultAsync();

                return log.StackTrace;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public async Task<PagedListResponse<IApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string Message, string Source, string Channel, int PageNumber = 0, int PageSize = 50)
        {
            var filterBuilder = Builders<ApplicationErrorLog>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.LogDate, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.LogDate, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(userId))
            {
                var userFilter = filterBuilder.Eq(i => i.UserId, userId);
                filter = filter & userFilter;
            }

            if (!String.IsNullOrWhiteSpace(Source))
            {
                var loggerFilter = filterBuilder.Eq(i => i.Logger, Source);
                filter = filter & loggerFilter;
            }

            if (!String.IsNullOrWhiteSpace(Channel))
            {
                var loggerFilter = filterBuilder.Eq(i => i.Channel, Channel);
                filter = filter & loggerFilter;
            }

           

            if (!String.IsNullOrWhiteSpace(Message))
            {
                var textFilter = filterBuilder.ElemMatch(i => i.ExceptionMessage, Message);
                filter = filter & textFilter;
            }

            var fields = Builders<ApplicationErrorLog>.Projection.Exclude(e => e.StackTrace);

            var list = await this.GetQuery<ApplicationErrorLog>().Find(filter).Project<ApplicationErrorLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.LogDate).ToListAsync();

            var count = await this.GetQuery<ApplicationErrorLog>().CountDocumentsAsync(filter);


            return new PagedListResponse<IApplicationErrorLog>(list, PageNumber, PageSize, count);
        }
    }
}
