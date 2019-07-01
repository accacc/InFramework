using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IF.Core.Performance;
using IF.MongoDB.Repository.Abstract;

namespace IF.MongoDB
{
    public interface IMongoPerformanceLogRepository:IRepository
    {       


        Task<IEnumerable<PerformanceLogLowStat>> GetLowPerformanceLogsAsync();
    }
}
