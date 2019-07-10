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

   

    public class SmsBulkOneToManyOperationMongoDb: ISmsBulkOneToManyOperation
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        //public IFormFile File { get; set; }
        public string BulkName { get; set; }

        public int SplitBy { get; set; }

        public long Total { get; set; }
        public string Message { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public SmsOperationStatus Status { get; set; }


    }

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
