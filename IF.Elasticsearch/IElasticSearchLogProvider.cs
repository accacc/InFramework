using IF.Core.Elasticsearch;
using IF.Elasticsearch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Elasticsearch
{
    public interface IElasticsearchApplicationLogProvider : IElasticSearchLogProvider<ApplicationErrorLog >
    {

    }
}
