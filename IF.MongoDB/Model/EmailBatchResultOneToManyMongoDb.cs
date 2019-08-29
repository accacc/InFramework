using IF.Core.Sms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace IF.MongoDB.Model
{
    public class EmailBatchResultOneToManyMongoDb : ISmsBatchResult
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
