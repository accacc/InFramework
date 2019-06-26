using IF.Core.Common;
using IF.Core.RabbitMQ;
using IF.HealthChecks.Checks;
using RabbitMQ.Client;
using System;

namespace IF.HealthChecks.RabbitMQ
{

    public static class HealthCheckBuilderRabbitMQExtensions
    {
        public static HealthCheckBuilder AddRabbitMQCheck(this HealthCheckBuilder builder, string name, RabbitMQConnectionSettings settings)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddRabbitMQCheck(builder, name, settings, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddRabbitMQCheck(this HealthCheckBuilder builder, string name, RabbitMQConnectionSettings settings, TimeSpan cacheDuration)
        {
            builder.AddCheck($"RabbitMQCheck({name})", () =>
            {
                try
                {
                    
                    var factory = new ConnectionFactory()
                    {
                        HostName = settings.EventBusConnection
                    };

                    if (!string.IsNullOrEmpty(settings.EventBusUserName))
                    {
                        factory.UserName = settings.EventBusUserName;
                    }

                    if (!string.IsNullOrEmpty(settings.EventBusPassword))
                    {
                        factory.Password = settings.EventBusPassword;
                    }


                    factory.Port = 5672;

                    if (!string.IsNullOrEmpty(settings.Port))
                    {
                        factory.Port = int.Parse(settings.Port);

                    }

                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {

                        return HealthCheckResult.Healthy($"RabbitMQCheck({name}): Healthy");
                    }

                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"RabbitMQCheck({name}): Exception during check: {ex.GetType().FullName}");
                }
            }, cacheDuration);

            return builder;
        }
    }
}
