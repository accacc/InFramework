using IF.Core.Data;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoLogRepository: IRepository
    {
        Task<string> GetStackTraceAsync(Guid id);    

        Task<PagedListResponse<ApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId,string Message, string Source, string Channel, int skipNumber = 0, int takeNumber = 50);


    }
}
