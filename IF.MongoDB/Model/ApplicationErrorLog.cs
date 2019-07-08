using IF.Core.Log;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class ApplicationErrorLogMongoDB:IApplicationErrorLog
    {

        [BsonId]
        public ObjectId InternalId { get; set; }

        public string Channel { get; set; }

        public Guid UniqueId { get; set; }

        public string ExceptionMessage { get; set; }

        public string Logger { get; set; }

        public string UserId { get; set; }

        public string StackTrace { get; set; }

        public string Message { get; set; }

        public string Level { get; set; }

        public string MachineName { get; set; }

        public string IPAddress { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]


        public DateTime LogDate { get; set; } = DateTime.Now;
    }


   

   


    
}
