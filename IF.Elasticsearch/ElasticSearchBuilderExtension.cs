using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using IF.Elasticsearch.Model;
using IF.Core.Elasticsearch;
using IF.Core.DependencyInjection;
using IF.Elasticsearch.Repository;
using IF.Core.Log;

namespace IF.Elasticsearch
{
    public static class BuilderExtensions
    {
        public static ILoggerBuilder AddElasticSearchLogger(this ILoggerBuilder logger, ElasticSearchLogSettings settings)
        {
            logger.Builder.RegisterInstance<IElasticsearchApplicationLogProvider>(new ElasticsearchApplicationLogProvider(settings,new ElasticSearchLoggingConnectionStrategy()), DependencyScope.PerRequest);
            logger.Builder.RegisterType<ElasticSearchLogService, ILogService>(DependencyScope.PerRequest);
            return logger;
        }
    }
}
