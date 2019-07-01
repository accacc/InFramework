using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface ICqrsBuilder
    {
        //ICqrsBuilder AddCqrs();
        IQueryHandlerBuilder AddQueryHandlers();

        IQueryHandlerBuilder AddQueryAsyncHandlers();

        ICommandHandlerBuilder AddCommandHandlers();        

        ICommandHandlerBuilder AddCommandAsyncHandlers();

        IQueryHandlerBuilder AddElasticQueryAsyncHandlers();
    }
}
