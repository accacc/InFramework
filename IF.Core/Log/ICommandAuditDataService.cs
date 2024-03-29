﻿using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Log
{
    public interface ICommandAuditDataService
    {
        Task LogAsync(object @object, Guid uniqueId, DateTime LogDate, string objectName, string IpAdress, string Channel, string UserId);

        void Log(object @object, Guid uniqueId, DateTime LogDate, string objectName, string IpAdress, string Channel, string UserId);
               

        Task<AuditLog> GetDetailAsync(Guid uniqueId);

        Task<PagedListResponse<AuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId, int PageNumber = 0, int PageSize = 50);
    }
}
