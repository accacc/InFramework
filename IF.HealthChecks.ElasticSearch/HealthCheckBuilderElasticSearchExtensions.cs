using IF.Core.Common;
using IF.HealthChecks.Checks;
using Microsoft.AspNetCore.Http;
using Nest;
using System;

namespace IF.HealthChecks.ElasticSearch
{

    public static class HealthCheckBuilderElasticSearchExtensions
    {
        public static HealthCheckBuilder AddElasticSearchLoggerCheck(this HealthCheckBuilder builder, string name, string host)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddElasticSearchLoggerCheck(builder, name, host, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddElasticSearchLoggerCheck(this HealthCheckBuilder builder, string name,string host, TimeSpan cacheDuration)
        {
            builder.AddCheck($"ElasticSearchLoggerCheck({name})", () =>
            {
                try
                {
                    var client = new ElasticClient(new Uri(host));
                    var result = client.Ping();
                    var success = result.ApiCall.HttpStatusCode == StatusCodes.Status200OK;

                    if(success)
                    {
                        return HealthCheckResult.Healthy($"ElasticSearchLoggerCheck({name}): Healthy");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy($"ElasticSearchLoggerCheck({name})");
                    }

                }
                catch (Exception ex)
                {

                    return HealthCheckResult.Unhealthy($"ElasticSearchLoggerCheck({name}): Exception during check: {ex.Message}");
                }
            }, cacheDuration);

            return builder;
        }
    }
}
