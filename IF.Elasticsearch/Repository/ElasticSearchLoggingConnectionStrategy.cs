using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace IF.Elasticsearch.Repository
{
    public class ElasticSearchLoggingConnectionStrategy : IElasticSearchConnectionStrategy
    {
        public ElasticClient GetConnection(string url,string defaultIndex)
        {
            var pool = new StaticConnectionPool(new List<Uri> { new Uri(url) });
            var connectionSettings = new ConnectionSettings(
                pool,
                new HttpConnection())
                .DefaultIndex(defaultIndex)
              .DisableDirectStreaming();

            return new ElasticClient(connectionSettings);
        }
    }
}
