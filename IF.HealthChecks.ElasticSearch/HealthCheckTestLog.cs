using IF.Core.Elasticsearch;
using IF.Elasticsearch.Repository;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.HealthChecks.ElasticSearch
{
    public class HealthCheckTestLog : IElasticSearchLog
    {

        public HealthCheckTestLog()
        {
        }
        public string ExceptionMessage { get; set; }
        public string Logger { get; set; }
        public string UserId { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }

        [Keyword]
        public Guid UniqueId { get; set; }


    }
}
