using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    //public interface IDbGetQueryAsync<TResult> : IDbQueryAsync
    //{
    //    Task<TResult> GetAsync();
    //}

    public interface IDbGetQueryAsync<TRequest, TResponse> : IDbQueryAsync
         where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
         Task<TResponse> GetAsync(TRequest request);
    }
}
