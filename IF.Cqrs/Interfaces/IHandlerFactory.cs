using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Interfaces
{


    
    public interface IHandlerFactory
    {
        IQueryHandler<TRequest, TResponse> Resolve<TRequest, TResponse>() where TResponse : BaseResponse, new() where TRequest : BaseRequest;
        ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : BaseCommand;

        IQueryHandlerAsync<TRequest, TResponse> ResolveAsync<TRequest, TResponse>() where TResponse : BaseResponse, new() where TRequest : BaseRequest;
        ICommandHandlerAsync<TCommand> ResolveAsync<TCommand>() where TCommand : BaseCommand;
    }


   
}
