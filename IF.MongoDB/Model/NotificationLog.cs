using IF.Core.Notification;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Model
{
    public class NotificationLog: INotificationLog
    {

        [BsonId]
        public ObjectId InternalId { get; set; }

        public string Device { get; set; }

        public string Message { get; set; }
        public bool Success { get; set; }
        public string Response { get; set; }

        public Guid UniqueId { get; set; }

        public Guid SourceId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
