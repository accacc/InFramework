using IF.Core.DependencyInjection.Model;
using IF.Core.Notification;
using IF.Core.Sms;
using System;
using Microsoft.Extensions.DependencyInjection;
namespace IF.Sms.Turatel.Integration
{
    public static class Extension
    {

        public static ISmsBuilder AddTuratel(this ISmsBuilder smsBuilder, IServiceCollection services, IFSmsSettings settings)
        {

            services.AddSingleton<IIFSmsOneToManyServiceAsync, TuratelSmsService>();
            services.AddSingleton<IIFSmsManyToManyServiceAsync, TuratelSmsService>();
            services.AddSingleton<IIFSmsOneToManyReportServiceAsync, TuratelSmsService>();            
            services.AddHttpClient<TuratelSmsClient>();
            services.AddSingleton(settings);
            return smsBuilder;
        }
    }
}
