using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Notification
{
    public interface INotificationLog
    {
        string Device { get; set; }
        //string Message { get; set; }
        bool Success { get; set; }
        string Response { get; set; }

        Guid UniqueId { get; set; }

        DateTime Date { get; set; }

        Guid SourceId { get; set; }
    }
}
