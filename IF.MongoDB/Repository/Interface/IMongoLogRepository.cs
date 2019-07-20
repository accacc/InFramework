using IF.Core.Data;
using IF.Core.Log;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface  IMongoApplicationLogRepository: IMongoDbGenericRepository
    {
        Task<string> GetStackTraceAsync(Guid id);

        Task<PagedListResponse<ApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string Message, string Source, string Channel, int PageNumber = 0, int PageSize = 50);


    }
}
