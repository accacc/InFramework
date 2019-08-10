using IF.Core.Configuration;
using IF.Core.Data;
using IF.Core.Data;
using IF.Core.Json;
using IF.Core.Log;
using IF.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Query
{
    public class IdentityQueryDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse> where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
        private readonly IIdentityService identityService;
        //private readonly IAppSettings appSettings;


        public IdentityQueryDecorator(IQueryHandler<TRequest, TResponse> queryHandler, IIdentityService identityService)
        {
            this.queryHandler = queryHandler;
            this.identityService = identityService;
            //this.appSettings = appSettings;

        }

        public TResponse Handle(TRequest request)
        {


            this.identityService?.SetUserProfile(request);

            TResponse response = queryHandler.Handle(request);

            return response;

        }

     
    }
}
