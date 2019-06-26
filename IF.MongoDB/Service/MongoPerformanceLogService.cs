using IF.Core.Log;
using IF.Core.Performance;
using System;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoPerformanceLogService : IPerformanceLogService
    {

        private readonly IMongoPerformanceLogRepository mongoLogRepository;

        public MongoPerformanceLogService(IMongoPerformanceLogRepository mongoLogRepository)
        {
            this.mongoLogRepository = mongoLogRepository;
        }

        public void Log(DateTime ExecutionDate, double ExecutionTime, string MethodName, Guid uniqueId)
        {
            PerformanceLog performanceLog = new PerformanceLog();
            performanceLog.ExecutionDate = ExecutionDate;
            performanceLog.ExecutionTime = ExecutionTime;
            performanceLog.MethodName = MethodName;
            performanceLog.UniqueId = uniqueId;
            this.mongoLogRepository.AddLog(performanceLog);
        }

        public async Task LogAsync(DateTime ExecutionDate, double ExecutionTime, string MethodName, Guid uniqueId)
        {

            PerformanceLog performanceLog = new PerformanceLog();
            performanceLog.ExecutionDate = ExecutionDate;
            performanceLog.ExecutionTime = ExecutionTime;
            performanceLog.MethodName = MethodName;
            performanceLog.UniqueId = uniqueId;
            await this.mongoLogRepository.AddLogAsync(performanceLog);
        }
    }
}
