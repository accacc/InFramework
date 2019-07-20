using IF.Core.Data;
using IF.Core.Log;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public interface IMongoAuditLogRepository:IMongoDbGenericRepository
    {
        


        Task<IAuditLog> GetDetailAsync(Guid uniqueId);

        Task<PagedListResponse<IAuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId,int PageNumber = 0, int PageSize = 50);
        
    }
}
