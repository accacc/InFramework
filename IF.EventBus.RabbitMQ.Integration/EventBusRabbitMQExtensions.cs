using Autofac;
using IF.Core.DependencyInjection;
using IF.Core.EventBus;
using IF.Core.Log;
using IF.Core.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using IF.Core.Exception;
using IF.Core.Common;
using System.Data.Common;
using IF.EventBus.Logging.EF;
using IF.Core.EventBus.Log;

namespace IF.EventBus.RabbitMQ.Integration
{
    public static class EventBusRabbitMQExtensions
    {
        public static IEventBusLogBuilder AddRabbitMQEventBus(this IEventBusLogBuilder eventBusBuilder, IServiceCollection services,RabbitMQConnectionSettings settings,string applicationName)
        {
            

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {

                Guard.ArgumentNotNullOrEmpty(nameof(applicationName),applicationName);

                if (String.IsNullOrWhiteSpace(settings.EventBusConnection))
                {
                    throw new IFApplicationException(applicationName +  " : Cannot read connection information for RabbitMQ");
                }

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

                var retryCount = 5;

                if (!string.IsNullOrEmpty(settings.EventBusRetryCount))
                {
                    retryCount = int.Parse(settings.EventBusRetryCount);
                }

                factory.Port = 5672;

                if (!string.IsNullOrEmpty(settings.Port))
                {
                    factory.Port = int.Parse(settings.Port);
                }

                factory.RequestedHeartbeat = 10;

                var logger = sp.GetRequiredService<ILogService>();

                return new DefaultRabbitMQPersistentConnection(factory,logger, retryCount);
            });

           

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
               
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    //var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;

                if (!string.IsNullOrEmpty(settings.EventBusRetryCount))
                {
                    retryCount = int.Parse(settings.EventBusRetryCount);
                }

                var logger = sp.GetRequiredService<ILogService>();

                var eventLogger = sp.GetRequiredService<IEventLogService>();

                return new EventBusRabbitMQ(eventLogger,rabbitMQPersistentConnection, iLifetimeScope, eventBusSubcriptionsManager, logger, applicationName, retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return eventBusBuilder;

        }




    }

    
}
