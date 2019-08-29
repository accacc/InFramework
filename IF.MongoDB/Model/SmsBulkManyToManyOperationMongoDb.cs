//using System;
//using IF.Core.Sms;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using IF.Core.Sms.Interface;

//namespace IF.MongoDB.Model
//{
//    public class SmsBulkManyToManyOperationMongoDb : ISmsBulkManyToManyOperation
//    {
//        [BsonId]
//        public ObjectId InternalId { get; set; }

//        //public IFormFile File { get; set; }
//        public string BulkName { get; set; }

//        public int SplitBy { get; set; }

//        public long Total { get; set; }
//        //public string Message { get; set; }

//        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
//        public DateTime CreatedDate { get; set; } = DateTime.Now;

//        public SmsOperationStatus Status { get; set; }

//        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
//        public DateTime UpdatedDate { get; set; } = DateTime.Now;

//        public int BatchCount { get; set; }

//        public string SenderPrefixName { get; set; }

//        public string CallBackPrefixName { get; set; }

//        public string CallBackMessageTemplate { get; set; }

//        public string CallBackNumberId { get; set; }

//        public DateTime? StartDate { get; set; }

//        public DateTime? EndDate { get; set; }
//    }
//}
