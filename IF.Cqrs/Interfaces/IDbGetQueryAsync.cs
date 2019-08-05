using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
   

    public interface IDbGetQueryAsync<TRequest, TResponse> : IDbQueryAsync
         where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
         Task<TResponse> GetAsync(TRequest request);
    }
}
