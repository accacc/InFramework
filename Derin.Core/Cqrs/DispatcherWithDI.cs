using IF.Core.Handler;
using IF.Core.Exception;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using IF.Core.Security;

namespace Derin.Core.Cqrs
{
    public class DispatcherWithDI : IDispatcher
    {

        IHandlerFactory handlerFactory;
        IIdentityService identityService;
        public DispatcherWithDI(IHandlerFactory handlerFactory, IIdentityService identityService = null)
        {
            this.handlerFactory = handlerFactory;
            this.identityService = identityService;
        }

        public  async Task CommandAsync<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            var commandHandler = handlerFactory.ResolveAsync<TCommand>();

            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException();
            }

            this.identityService?.SetUserProfile(command);

            await commandHandler.HandleAsync(command);
        }

        

        public void Command<TCommand>(TCommand command) where TCommand : BaseCommand
        {

            var commandHandler = handlerFactory.Resolve<TCommand>();

            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException();
            }

            this.identityService?.SetUserProfile(command);

            commandHandler.Handle(command);


        }


        public async Task<TResponse> QueryAsync<TRequest, TResponse>(TRequest request) where TResponse : BaseResponse, new() where TRequest : BaseRequest
        {


            var queryHandler = handlerFactory.ResolveAsync<TRequest, TResponse>();

            if (queryHandler == null)
            {
                throw new QueryHandlerNotFoundException();
            }

            this.identityService?.SetUserProfile(request);

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

            this.identityService?.SetUserProfile(request);

            TResponse result = queryHandler.Handle(request);


            return result;

        }

        
    }
}
