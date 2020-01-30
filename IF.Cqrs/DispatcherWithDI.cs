using IF.Core.Data;
using IF.Cqrs.Exception;
using System.Threading.Tasks;

namespace IF.Cqrs
{
    public class DispatcherWithDI : IDispatcher
    {

        IHandlerFactory handlerFactory;
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


            await commandHandler.HandleAsync(command).ConfigureAwait(false);
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


            var result = await queryHandler.HandleAsync(request).ConfigureAwait(false); ;


            return result;

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
