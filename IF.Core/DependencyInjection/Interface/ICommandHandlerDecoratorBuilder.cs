using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface ICommandHandlerDecoratorBuilder
    {
        ICommandHandlerDecoratorBuilder AddAuditing();
        ICommandHandlerDecoratorBuilder AddDataAnnonationValidation();

        ICommandHandlerDecoratorBuilder AddErrorLogging();

        ICommandHandlerDecoratorBuilder AddPerformanceCounter();


        ICommandHandlerDecoratorBuilder AddIdentity();
        ICommandHandlerDecoratorBuilder AddCustomCommandHandler<T>();

    }
}
