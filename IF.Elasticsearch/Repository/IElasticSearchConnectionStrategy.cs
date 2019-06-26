using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Elasticsearch.Repository
{
    public interface IElasticSearchConnectionStrategy
    {
        ElasticClient GetConnection(string url, string defaultIndex);
    }
}
