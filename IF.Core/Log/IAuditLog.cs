using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Log
{
    public interface IAuditLog
    {
        Guid UniqueId { get; set; }

        DateTime LogDate { get; set; }


        string ObjectName { get; set; }


        string JsonObject { get; set; }

        string UserId { get; set; }

        string Channel { get; set; }

        string ClientId { get; set; }
    }
}
