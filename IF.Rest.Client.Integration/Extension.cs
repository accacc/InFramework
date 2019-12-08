using IF.Core.DependencyInjection.Interface;
using IF.Core.Rest;
using Microsoft.Extensions.DependencyInjection;

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
