using IF.Core.Data;
using IF.MongoDB.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{

    public class MongoSmsLogRepository : IMongoSmsLogRepository
    {
        private readonly LogContext _context = null;

        public MongoSmsLogRepository(string url, string db)
        {
            _context = new LogContext(url, db);
        }

        public async Task<IEnumerable<SmsLog>> GetAllLogsAsync()
        {
            try
            {
                return await _context.SmsLogs.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SmsLog> GetLogsAsync(Guid id)
        {
            try
            {
                return await _context.SmsLogs
                                .Find(log => log.UniqueId == id)
                                .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public async Task<IEnumerable<SmsLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.SmsLogs.Find(log => log.Message.Contains(bodyText) &&
                                       log.Date >= updatedFrom).SortBy(s => s.Date);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task AddLogAsync(SmsLog item)
        {
            try
            {
                await _context.SmsLogs.InsertOneAsync(item);
            }
            catch// (Exception ex)
            {
                //throw ex;
            }
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

         



            var list = await _context.SmsLogs.Find(filter).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await _context.SmsLogs.CountDocumentsAsync(filter);


            return new PagedListResponse<SmsLog>(list, PageNumber, PageSize, count);
        }

    }
}
