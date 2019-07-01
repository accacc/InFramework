using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Performance
{
    public interface IPerformanceLogService
    {
        Task LogAsync(DateTime ExecutionDate, double ExecutionTime, string MethodName, Guid uniqueId);
        void Log(DateTime ExecutionDate, double ExecutionTime, string MethodName, Guid uniqueId);

        Task<IEnumerable<PerformanceLogLowStat>> GetLowPerformanceLogsAsync();

    }
}
