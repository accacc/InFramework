using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface ICommandHandlerBuilder
    {
        ICommandHandlerBuilder Decoration(Action<ICommandHandlerDecoratorBuilder> action);

        IInFrameworkBuilder Build(Assembly[] assemblies);
    }
}
