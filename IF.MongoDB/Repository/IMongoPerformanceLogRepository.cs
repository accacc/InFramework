using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IF.Core.Performance;

namespace IF.MongoDB
{
    public interface IMongoPerformanceLogRepository
    {
        Task<IEnumerable<PerformanceLog>> GetAllLogsAsync();
        Task<PerformanceLog> GetLogAsync(Guid id);

        Task<IEnumerable<PerformanceLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        Task AddLogAsync(PerformanceLog log);

        void AddLog(PerformanceLog log);

        Task<IEnumerable<PerformanceLogLowStat>> GetLowPerformanceLogsAsync();
    }
}
