﻿using IF.Core.Data;
using IF.Core.Email;
using IF.Core.MongoDb;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Interface
{
    public interface IMongoEmailLogRepository:IMongoDbGenericRepository
    {
        

        
        Task<PagedListResponse<IEmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int PageNumber = 0, int PageSize = 50);

        Task<string> GetBodyAsync(Guid id);

    }
}
