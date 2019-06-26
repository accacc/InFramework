using IF.Core.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoLogRepository : IMongoLogRepository
    {
        private readonly LogContext _context = null;

        public MongoLogRepository(string url, string db)
        {
            _context = new LogContext(url, db);
        }

        public async Task<IEnumerable<ApplicationErrorLog>> GetAllLogsAsync()
        {
            try
            {
                return await _context.Logs.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApplicationErrorLog> GetLogAsync(Guid id)
        {
            try
            {
                return await _context.Logs
                                .Find(log => log.UniqueId == id)
                                .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public async Task<IEnumerable<ApplicationErrorLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.Logs.Find(log => log.ExceptionMessage.Contains(bodyText) &&
                                       log.LogDate >= updatedFrom).SortByDescending(s => s.LogDate); 

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task AddLogAsync(ApplicationErrorLog item)
        {
            try
            {
                await _context.Logs.InsertOneAsync(item);
            }
            catch //(Exception ex)
            {
                //throw ex;
            }
        }

        
        public async Task<string> GetStackTraceAsync(Guid id)
        {

            try
            {
                var fields = Builders<ApplicationErrorLog>.Projection.Include(e => e.StackTrace);

                var log = await _context.Logs
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

        public async Task<PagedListResponse<ApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string Message, string Source, string Channel, int PageNumber = 0, int PageSize = 50)
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

            var list = await _context.Logs.Find(filter).Project<ApplicationErrorLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.LogDate).ToListAsync();

            var count = await _context.Logs.CountDocumentsAsync(filter);


            return new PagedListResponse<ApplicationErrorLog>(list, PageNumber, PageSize, count);
        }
    }
}
