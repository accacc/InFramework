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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{


    


    public class MongoApplicationLogRepository : MongoDbGenericRepository, IMongoApplicationLogRepository
    {


        //public MongoApplicationLogRepository(MongoConnectionSettings settings) : base(settings)
        //{

        //}
        public MongoApplicationLogRepository(IMongoDbConnectionStrategy connectionStrategy) :base(connectionStrategy)
        {

        }

        public async Task<string> GetStackTraceAsync(Guid id)
        {

          
                var fields = Builders<ApplicationErrorLogMongoDB>.Projection.Include(e => e.StackTrace);

                var log = await this.GetQuery<ApplicationErrorLogMongoDB>(nameof(ApplicationErrorLog))
                                .Find(e => e.UniqueId == id)
                                .Project<ApplicationErrorLogMongoDB>(fields)
                                .SingleOrDefaultAsync();

                return log.StackTrace;
          

            
        }

        public async Task<PagedListResponse<ApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string Message, string Source, string Channel, int PageNumber = 0, int PageSize = 50)
        {
            var filterBuilder = Builders<ApplicationErrorLogMongoDB>.Filter;
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

            var fields = Builders<ApplicationErrorLogMongoDB>.Projection.Exclude(e => e.StackTrace).Exclude("_id"); 

            var list = await this.GetQuery<ApplicationErrorLogMongoDB>(nameof(ApplicationErrorLog)).Find(filter).Project<ApplicationErrorLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).ToListAsync();

            var count = await this.GetQuery<ApplicationErrorLogMongoDB>(nameof(ApplicationErrorLog)).CountDocumentsAsync(filter);


            return new PagedListResponse<ApplicationErrorLog>(list, PageNumber, PageSize, count);
        }
    }
}
