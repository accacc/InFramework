using Autofac;
using IF.Core.Common;
using IF.Core.DependencyInjection.Interface;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using IF.Core.Exception;
using IF.Core.Log;
using IF.Core.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IF.EventBus.RabbitMQ.Integration
{
    public static class EventBusRabbitMQExtensions
    {
        public static IEventBusLogBuilder AddRabbitMQEventBus(this IEventBusLogBuilder eventBusBuilder, IServiceCollection services,RabbitMQConnectionSettings settings,string applicationName)
        {


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {

                Guard.ArgumentNotNullOrEmpty(nameof(applicationName), applicationName);

                if (String.IsNullOrWhiteSpace(settings.EventBusConnection))
                {
                    throw new IFApplicationException(applicationName + " : Cannot read connection information for RabbitMQ");
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

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
            return InitEventBus(eventBusBuilder, services, settings.EventBusRetryCount, applicationName);

        }


        public static IEventBusLogBuilder AddRabbitMQEventBus(this IEventBusLogBuilder eventBusBuilder, IServiceCollection services, RabbitMQTcpConnectionSettings settings, string applicationName)
        {


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {

                Guard.ArgumentNotNullOrEmpty(nameof(applicationName), applicationName);

                if (settings.Servers==null && !settings.Servers.Any())
                {
                    throw new IFApplicationException(applicationName + " : Cannot read connection information for RabbitMQ");
                }


                var endpoints = new List<AmqpTcpEndpoint>();
                var ssl = new SslOption();
                ssl.Enabled = settings.IsSslEnabled;

                int defaultPort =  5672;

                foreach (var server in settings.Servers)
                {
                    int currentPort = 0;

                    if (server.Port == 0)
                    {
                        currentPort = defaultPort;
                    }
                    else
                    {
                        currentPort = server.Port;
                    }

                    endpoints.Add(new AmqpTcpEndpoint {HostName = server.EventBusConnection,Port = currentPort,Ssl = ssl });
                }

                var factory = new ConnectionFactory();
                

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

                

                factory.RequestedHeartbeat = 10;

                var logger = sp.GetRequiredService<ILogService>();

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


            return InitEventBus(eventBusBuilder, services, settings.EventBusRetryCount, applicationName);

        }

        private static IEventBusLogBuilder InitEventBus(IEventBusLogBuilder eventBusBuilder, IServiceCollection services, string RetryCount, string applicationName)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {

                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                //var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;

                if (!string.IsNullOrEmpty(RetryCount))
                {
                    retryCount = int.Parse(RetryCount);
                }

                var logger = sp.GetRequiredService<ILogService>();

                var eventLogger = sp.GetRequiredService<IEventLogService>();

                return new EventBusRabbitMQ(eventLogger, rabbitMQPersistentConnection, iLifetimeScope, eventBusSubcriptionsManager, logger, applicationName, retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return eventBusBuilder;
        }
    }

    
}
