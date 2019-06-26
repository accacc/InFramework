using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    public interface IQueryHandler<TRequest,TResponse> 
        where TRequest:BaseRequest
        where TResponse :BaseResponse
    {
        TResponse Handle(TRequest request);
    }

    public interface ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        void Handle(TCommand command);
    }

  

}