using IF.Core.Email;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Model
{
    public class EmailLog: IEmailLog
    {

        [BsonId]
        public ObjectId InternalId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; } = DateTime.Now;
        public string Type { get; set; }
        public bool IsSent { get; set; }
        public string Subject { get; set; }

        public Guid UniqueId { get; set; }
        public Guid SourceId { get; set; }
    }
}
