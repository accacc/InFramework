﻿using IF.Core.DependencyInjection.Interface;
using IF.Core.Notification;
using IF.Core.Sms;
using System;
using Microsoft.Extensions.DependencyInjection;
using IF.Core.Sms.Interface;

namespace IF.Sms.Turatel.Integration
{
    public static class Extension
    {

        public static ISmsBuilder AddTuratel(this ISmsBuilder smsBuilder, IServiceCollection services, IFSmsSettings settings)
        {

            services.AddSingleton<IIFSmsOneToManyServiceAsync, TuratelSmsService>();
            services.AddSingleton<IIFSmsManyToManyServiceAsync, TuratelSmsService>();
            services.AddSingleton<IIFSmsStatusServiceAsync, TuratelSmsService>();
            services.AddSingleton<IIFSmsCallbackServiceAsync, TuratelSmsService>();
            services.AddHttpClient<TuratelSmsClient>();
            services.AddSingleton(settings);
            return smsBuilder;
        }
    }
}
