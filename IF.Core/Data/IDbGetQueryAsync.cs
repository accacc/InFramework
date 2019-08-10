using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
   

    public interface IDataGetQueryAsync<TRequest, TResponse> : IDataQueryAsync
         where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
         Task<TResponse> GetAsync(TRequest request);
    }
}
