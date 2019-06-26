using IF.Core.Log;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Model
{
    public class AuditLog:IAuditLog
    {

        [BsonId]
        public ObjectId InternalId { get; set; }

        public Guid UniqueId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]


        public DateTime LogDate { get; set; } = DateTime.Now;


        public string ObjectName { get; set; }


        public string JsonObject { get; set; }


        public string Channel { get; set; }

        public string ClientId { get; set; }

        public string UserId { get; set; }
    }
}
