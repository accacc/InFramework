using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface IHandlerBuilder
    {
        //ICqrsBuilder AddCqrs();
        IQueryHandlerBuilder AddQueryHandlers();

        IQueryHandlerBuilder AddQueryAsyncHandlers();

        ICommandHandlerBuilder AddCommandHandlers();        

        ICommandHandlerBuilder AddCommandAsyncHandlers();

        IQueryHandlerBuilder AddElasticQueryAsyncHandlers();
    }
}
