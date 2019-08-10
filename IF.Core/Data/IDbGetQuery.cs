using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface IDataGetQuery<TRequest, TResponse> : IDataQuery
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        TResponse Get(TRequest request);
    }
}
