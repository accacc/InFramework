using IF.MongoDB.Repository.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoPerformanceLogRepository : GenericRepository, IMongoPerformanceLogRepository
    {


        public MongoPerformanceLogRepository(string url, string db) : base(url, db)
        {

        }

        public async Task<IEnumerable<PerformanceLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {

            var query = this.GetQuery<PerformanceLog>().Find(log => log.LogDate >= updatedFrom).SortByDescending(s => s.LogDate);

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<PerformanceLogLowStat>> GetLowPerformanceLogsAsync()
        {


            var result = await this.GetQuery<PerformanceLog>().Aggregate().Group(
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


    }



}

