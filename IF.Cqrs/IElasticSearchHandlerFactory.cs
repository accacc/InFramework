using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Handler
{
    public interface IElasticSearchHandlerFactory
    {

        IElasticQueryHandlerAsync<TRequest, TResponse> ResolveAsync<TRequest, TResponse>() where TResponse : BaseResponse, new() where TRequest : BaseRequest;
    }
}
