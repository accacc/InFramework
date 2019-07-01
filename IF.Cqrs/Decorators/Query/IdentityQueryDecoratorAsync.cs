using IF.Core.Configuration;
using IF.Core.Handler;
using IF.Core.Json;
using IF.Core.Log;
using IF.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace IF.Core.Cqrs.Decorators.Query
{

   
    public class IdentityQueryDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse> where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        private readonly IIdentityService identityService;
        //private readonly IAppSettings appSettings;


        public IdentityQueryDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, IIdentityService identityService)
        {
            this.queryHandler = queryHandler;
            this.identityService = identityService;
            //this.appSettings = appSettings;

        }

        public async Task<TResponse> HandleAsync(TRequest request)
        {
            this.identityService?.SetUserProfile(request);

            TResponse response = await queryHandler.HandleAsync(request);

            return response;


        }

      
    }
}
