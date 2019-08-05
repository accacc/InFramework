using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IF.Dependency.Builders
{
    public class QueryHandlerBuilder: IQueryHandlerBuilder
    {


        private readonly List<Type> handlers;

        private readonly IInFrameworkBuilder dependencyInjection;

        private readonly bool IsAsync;

        public QueryHandlerBuilder(IInFrameworkBuilder dependencyInjection,Type type,bool IsAsync = false)
        {
            this.handlers = new List<Type>();
            this.handlers.Add(type);
            this.dependencyInjection = dependencyInjection;
            this.IsAsync = IsAsync;
        }

        

        public IQueryHandlerBuilder Decoration(Action<IQueryHandlerDecoratorBuilder> action)
        {
            IQueryHandlerDecoratorBuilder decorator = new QueryHandlerDecoratorBuilder(handlers,IsAsync);

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
