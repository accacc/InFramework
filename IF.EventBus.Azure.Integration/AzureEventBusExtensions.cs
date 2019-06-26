using Autofac;
using IF.Core.EventBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IF.EventBus.Azure;

namespace IF.EventBus.Azure.Integration
{
    public static class AzureEventBusExtensions
    {
        public static IServiceCollection AddAzureEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            //IF.EventBus.Azure.AzureServiceBusConnectionSettings settings = configuration.GetSettings<AzureServiceBusConnectionSettings>();

            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                //var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                var serviceBusConnectionString = "";
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                return new DefaultServiceBusPersisterConnection(serviceBusConnection);
            });

            var subscriptionClientName = "";

            services.AddSingleton<IEventBus, AzureEventServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                //var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new AzureEventServiceBus(serviceBusPersisterConnection, eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

    }

}
