using IF.Core.Log;
using IF.Core.Performance;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB
{
    public class PerformanceLog : IPerformanceLog
    {

        [BsonId]
        public ObjectId InternalId { get; set; }
        public Guid UniqueId { get; set; }

        public string MethodName { get; set; }

        public double ExecutionTime { get; set; }
        public DateTime ExecutionDate { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime LogDate { get; set; } = DateTime.Now;
    }

   
}
