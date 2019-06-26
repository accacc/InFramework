using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoPerformanceLogRepository : IMongoPerformanceLogRepository
    {
        private readonly LogContext _context = null;

        public MongoPerformanceLogRepository(string url, string db)
        {
            _context = new LogContext(url, db);
        }

        public async Task<IEnumerable<PerformanceLog>> GetAllLogsAsync()
        {
            try
            {
                return await _context.PerformanceLogs.Find(_ => true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PerformanceLog> GetLogAsync(Guid id)
        {
            try
            {
                return await _context.PerformanceLogs
                                .Find(log => log.UniqueId == id)
                                .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public async Task<IEnumerable<PerformanceLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.PerformanceLogs.Find(log =>   log.LogDate >= updatedFrom).SortByDescending(s => s.LogDate); 

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PerformanceLogLowStat>> GetLowPerformanceLogsAsync()
        {
            try
            {
                //var max = await  _context.PerformanceLogs.Find(a => a.ExecutionTime > 1).SortByDescending(s => s.ExecutionTime).ToListAsync();

                var result = await _context.PerformanceLogs.Aggregate().Group(
                                        x => x.MethodName,
                                        g => new PerformanceLogLowStat()
                                        {
                                            MethodName = g.Key,
                                            Maximimum = g.Select(x => x.ExecutionTime).Max(),
                                            Minimum = g.Select(x => x.ExecutionTime).Min(),
                                            Avarage = g.Select(x => x.ExecutionTime).Average(),
                                            Count = g.Count()

                                        })
                                        .SortByDescending(s => s.Maximimum)
                                        .ToListAsync();

                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        public async Task AddLogAsync(PerformanceLog item)
        {
            try
            {
                await _context.PerformanceLogs.InsertOneAsync(item);
            }
            catch//(Exception ex)
            {
                //throw ex;
            }

            //await Task.FromResult(0);
        }

        public void AddLog(PerformanceLog log)
        {
            try
            {
                _context.PerformanceLogs.InsertOne(log);
            }
            catch//(Exception ex)
            {
                //throw ex;
            }
        }
    }
}
