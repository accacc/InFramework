using IF.Core.DependencyInjection.Interface;
using IF.Core.DependencyInjection.Model;
using IF.Core.Notification;
using IF.Core.OneSignal;
using IF.Core.Rest;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IF.Rest.Client.Integration
{
    public static class Extension
    {
        public static IRestClientBuilder AddFluent(this IRestClientBuilder restClientBuilder, IServiceCollection services)
        {

            services.AddHttpClient<IIFRestClient, IFRestClient>();
            services.AddSingleton<IIFFluentRestClient, IFFluentRestClient>();
            return restClientBuilder;
        }


    }
}
