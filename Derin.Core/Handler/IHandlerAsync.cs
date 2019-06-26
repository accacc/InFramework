using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
  

    public interface IQueryHandlerAsync<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        Task<TResponse> HandleAsync(TRequest request);
    }

    public interface ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        Task HandleAsync(TCommand command);
    }


}