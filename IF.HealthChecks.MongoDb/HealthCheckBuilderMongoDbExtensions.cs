using IF.Core.Common;
using IF.Core.RabbitMQ;
using IF.HealthChecks.Checks;
using MongoDB.Driver;
using System;

namespace IF.HealthChecks.RabbitMQ
{

    public static class HealthCheckBuilderMongoDbExtensions
    {
        public static HealthCheckBuilder AddMongoDbCheck(this HealthCheckBuilder builder, string name,string connectionString)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddMongoDbCheck(builder,name, connectionString, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddMongoDbCheck(this HealthCheckBuilder builder, string name, string connectionString, TimeSpan cacheDuration)
        {
            builder.AddCheck($"MongoDBCheck({name})", () =>
            {
                try
                {


                    new MongoClient(connectionString).ListDatabases();

                    return HealthCheckResult.Healthy($"MongoDBCheck({name}): Healthy");
                    

                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"MongoDBCheck({name}): Exception during check: {ex.GetType().FullName}");
                }
            }, cacheDuration);

            return builder;
        }
    }
}
