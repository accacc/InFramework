using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Handler
{
    public interface IDbGetQuery<TRequest, TResponse> : IDbQuery
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        TResponse Get(TRequest request);
    }
}
