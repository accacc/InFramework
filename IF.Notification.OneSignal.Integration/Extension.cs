using IF.Core.DependencyInjection.Model;
using IF.Core.Notification;
using IF.Core.OneSignal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IF.Notification.OneSignal.Integration
{
    public static class Extension
    {
        public static INotificationBuilder AddOneSignal(this INotificationBuilder notificationBuilder, IServiceCollection services, OneSignalApiSettings settings)
        {

            services.AddHttpClient<INotificationService, OneSignalNotificationApi>();
            services.AddSingleton(settings);
            return notificationBuilder;
        }


    }
}
