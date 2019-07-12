using IF.Core.DependencyInjection;
using IF.Core.Handler;

namespace IF.Dependency.AutoFac
{
    public class HandlerBuilder: IHandlerBuilder
    {
        private readonly IInFrameworkBuilder dependencyInjection;


        public HandlerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.dependencyInjection = dependencyInjection;
        }

        public IQueryHandlerBuilder AddQueryHandlers()
        {

            return new QueryHandlerBuilder(dependencyInjection, typeof(IQueryHandler<,>));
        }


        public ICommandHandlerBuilder AddCommandHandlers()
        {

            return new CommandHandlerBuilder(dependencyInjection, typeof(ICommandHandler<>));

        }


        public IQueryHandlerBuilder AddQueryAsyncHandlers()
        {

            return new QueryHandlerBuilder(dependencyInjection, typeof(IQueryHandlerAsync<,>), true);
        }


        public IQueryHandlerBuilder AddElasticQueryAsyncHandlers()
        {

            return new QueryHandlerBuilder(dependencyInjection, typeof(IElasticQueryHandlerAsync<,>), true);
        }


        public ICommandHandlerBuilder AddCommandAsyncHandlers()
        {

            return new CommandHandlerBuilder(dependencyInjection, typeof(ICommandHandlerAsync<>), true);

        }

    }
}
