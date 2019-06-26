using IF.Core.Sms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Model
{
    public class SmsLog: ISmsLog
    {

        [BsonId]
        public ObjectId InternalId { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        public bool IsSent { get; set; }

        public string Message { get; set; }


        public string Number { get; set; }


        public string Error { get; set; }
        public string IntegrationId { get; set; }
        public Guid UniqueId { get; set; }
        public Guid SourceId { get; set; }
    }
}
