using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
  

    public interface IQueryHandlerAsync<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        Task<TResponse> HandleAsync(TRequest request);
    }

    

  

   


}