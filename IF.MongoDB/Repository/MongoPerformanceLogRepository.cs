using IF.Core.MongoDb;
using IF.Core.Performance;
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


        public MongoPerformanceLogRepository(MongoConnectionSettings settings) : base(settings)
        {

        }

        public MongoPerformanceLogRepository(IMongoDbConnectionStrategy connectionStrategy) : base(connectionStrategy)
        {

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

