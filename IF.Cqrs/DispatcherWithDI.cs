using IF.Core.Exception;
using IF.Core.Data;
using IF.Core.Security;
using System.Threading.Tasks;
using IF.Cqrs.Exception;

namespace IF.Cqrs
{
    public class DispatcherWithDI : IDispatcher
    {

        IHandlerFactory handlerFactory;
        //IIdentityService identityService;
        public DispatcherWithDI(IHandlerFactory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public  async Task CommandAsync<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            var commandHandler = handlerFactory.ResolveAsync<TCommand>();

            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException();
            }


            await commandHandler.HandleAsync(command);
        }

        

        public void Command<TCommand>(TCommand command) where TCommand : BaseCommand
        {

            var commandHandler = handlerFactory.Resolve<TCommand>();

            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException();
            }


            commandHandler.Handle(command);


        }


        public async Task<TResponse> QueryAsync<TRequest, TResponse>(TRequest request) where TResponse : BaseResponse, new() where TRequest : BaseRequest
        {


            var queryHandler = handlerFactory.ResolveAsync<TRequest, TResponse>();

            if (queryHandler == null)
            {
                throw new QueryHandlerNotFoundException();
            }


            Task<TResponse> result = queryHandler.HandleAsync(request);


            return await result;

        }


        public TResponse Query<TRequest, TResponse>(TRequest request) where TResponse : BaseResponse, new() where TRequest : BaseRequest
        {


            var queryHandler = handlerFactory.Resolve<TRequest, TResponse>();

            if (queryHandler == null)
            {
                throw new QueryHandlerNotFoundException();
            }


            TResponse result = queryHandler.Handle(request);


            return result;

        }

        
    }
}
