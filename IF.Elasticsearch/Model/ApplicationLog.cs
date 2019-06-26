using IF.Core.Elasticsearch;
using IF.Core.Log;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Elasticsearch.Model
{
    public class ApplicationErrorLog : IElasticSearchLog, IApplicationErrorLog
    {
        public ApplicationErrorLog ()
        {
            Timestamp = DateTime.Now;
        }

        public string MachineName { get; set; }
        public string Channel { get; set; }

        public string IPAddress { get; set; }

        public string ExceptionMessage { get; set; }

        public string Logger { get; set; }

        public string UserId { get; set; }

        public string StackTrace { get; set; }

        public string Message { get; set; }

        public string Level { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime LogDate { get; set; }

        [Keyword]
        public Guid UniqueId { get; set; }
    }
}
