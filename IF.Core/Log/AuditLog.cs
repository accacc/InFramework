using System;

namespace IF.Core.Log
{
    public class AuditLog : IAuditLog
    {


        public Guid UniqueId { get; set; }

        public DateTime LogDate { get; set; }


        public string ObjectName { get; set; }


        public string JsonObject { get; set; }

        public string UserId { get; set; }

        public string Channel { get; set; }

        public string ClientId { get; set; }
    }
}
