using IF.Core.Log;
using IF.Core.Performance;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.MongoDB.Service
{
    public class MongoPerformanceLogService : IPerformanceLogService
    {

        private readonly IMongoPerformanceLogRepository mongoLogRepository;

        public MongoPerformanceLogService(IMongoPerformanceLogRepository mongoLogRepository)
        {
            this.mongoLogRepository = mongoLogRepository;
        }

        public async Task<IEnumerable<PerformanceLogLowStat>> GetLowPerformanceLogsAsync()
        {
            return await this.mongoLogRepository.GetLowPerformanceLogsAsync();
        }

        public void Log(DateTime ExecutionDate, double ExecutionTime, string MethodName, Guid uniqueId)
        {
            PerformanceLog performanceLog = new PerformanceLog();
            performanceLog.ExecutionDate = ExecutionDate;
            performanceLog.ExecutionTime = ExecutionTime;
            performanceLog.MethodName = MethodName;
            performanceLog.UniqueId = uniqueId;
            this.mongoLogRepository.Add(performanceLog);
        }

        public async Task LogAsync(DateTime ExecutionDate, double ExecutionTime, string MethodName, Guid uniqueId)
        {

            PerformanceLog performanceLog = new PerformanceLog();
            performanceLog.ExecutionDate = ExecutionDate;
            performanceLog.ExecutionTime = ExecutionTime;
            performanceLog.MethodName = MethodName;
            performanceLog.UniqueId = uniqueId;
            await this.mongoLogRepository.AddAsync(performanceLog);
        }
    }
}
