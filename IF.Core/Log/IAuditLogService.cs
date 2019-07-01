using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Log
{
    public interface IAuditLogService
    {
        Task LogAsync(object @object, Guid uniqueId, DateTime LogDate, string objectName, string IpAdress, string Channel, string UserId);

        void Log(object @object, Guid uniqueId, DateTime LogDate, string objectName, string IpAdress, string Channel, string UserId);
    }
}
