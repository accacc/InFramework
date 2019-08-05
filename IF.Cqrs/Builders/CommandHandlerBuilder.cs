using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IF.Dependency.Builders
{
    public class CommandHandlerBuilder: ICommandHandlerBuilder
    {


        private readonly List<Type> handlers;

        private readonly IInFrameworkBuilder dependencyInjection;

        private readonly bool IsAsync;

        public CommandHandlerBuilder(IInFrameworkBuilder dependencyInjection,Type type,bool IsAsync = false)
        {
            this.handlers = new List<Type>();
            this.handlers.Add(type);
            this.IsAsync = IsAsync;
            this.dependencyInjection = dependencyInjection;
        }

        public ICommandHandlerBuilder Decoration(Action<ICommandHandlerDecoratorBuilder> action)
        {
            ICommandHandlerDecoratorBuilder decorator = new CommandHandlerDecoratorBuilder(handlers, this.dependencyInjection, this.IsAsync);

            action(decorator);

            return this;
        }

        public IInFrameworkBuilder Build(Assembly[] assemblies)
        {
            dependencyInjection.RegisterDecorators(assemblies, this.handlers);
            return this.dependencyInjection;
        }
    }


   
}
