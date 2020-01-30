using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using IF.Core.Data;
using IF.Dependency.Builders;
using System;
using IF.Core.Data;

namespace IF.Cqrs.Builders
{
    public static class Extension
    {
        public static ICqrsBuilder AddHandler(this ICqrsBuilder cqrsBuilder,Action<IHandlerBuilder> action)
        {
            cqrsBuilder.Builder.RegisterType<DispatcherWithDI, IDispatcher>(DependencyScope.PerInstance);
            cqrsBuilder.Builder.RegisterAggregateService<IHandlerFactory>();
            //cqrsBuilder.Builder.RegisterAggregateService<IElasticSearchHandlerFactory>();
            action(new HandlerBuilder(cqrsBuilder.Builder));


            return cqrsBuilder;
        }
    }
}
