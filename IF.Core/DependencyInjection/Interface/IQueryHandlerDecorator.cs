using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface IQueryHandlerDecoratorBuilder
    {

        IQueryHandlerDecoratorBuilder AddAuditing();
        //IQueryHandlerDecoratorBuilder AddSimulatation();
        IQueryHandlerDecoratorBuilder AddErrorLogging();

        IQueryHandlerDecoratorBuilder AddIdentity();

        IQueryHandlerDecoratorBuilder AddPerformanceCounter();

        IQueryHandlerDecoratorBuilder AddSearching();
    }
}