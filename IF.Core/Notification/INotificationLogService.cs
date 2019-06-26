using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Notification
{
    public interface INotificationLogService
    {
        Task LogAsync(string DeviceId, bool Success,string Response,DateTime Date,  Guid UniqueId,Guid SourceId);
    }
}
