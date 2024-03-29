﻿using IF.Core.Data;
using IF.Core.Log;
using IF.Core.MongoDb;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Interface
{
    public interface IMongoAuditLogRepository:IMongoDbGenericRepository
    {
        Task<AuditLog> GetDetailAsync(Guid uniqueId);

        Task<PagedListResponse<AuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId, int PageNumber = 0, int PageSize = 50);


    }
}
