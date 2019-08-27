using System;
using System.Collections.Generic;
using System.Text;
using IF.Core.Sms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Model
{
    public class SmsBatchResultOneToManyMongoDb : ISmsBatchResultOneToMany
    {

        [BsonId]
        public ObjectId InternalId { get; set; }

        public int BatchNumber { get; set; }
        public string BatchName { get; set; }
        public int BatchCount { get; set; }

        public SmsOperationStatus Status { get; set; }

        public string ErrorCode { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid SourceId { get; set; }

        public string IntegrationId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
