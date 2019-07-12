using IF.Core.Cqrs;
using IF.Core.Cqrs.Decorators.Query;
using IF.Core.DependencyInjection;
using System;
using System.Collections.Generic;

namespace IF.Dependency.AutoFac
{
    public class QueryHandlerDecoratorBuilder : IQueryHandlerDecoratorBuilder
    {
        private readonly List<Type> handlers;
        private readonly bool IsAsync;

        public QueryHandlerDecoratorBuilder(List<Type> handlers, bool IsAsync = false)
        {
            this.handlers = handlers;
            this.IsAsync = IsAsync;
        }


        public IQueryHandlerDecoratorBuilder AddAuditing()
        {
            if (this.IsAsync)
            {
                this.handlers.Add(typeof(SaveAllDataQueryDecoratorAsync<,>));
            }
            else
            {
                this.handlers.Add(typeof(SaveAllDataQueryDecorator<,>));
            }



            return this;
        }

        //public IQueryHandlerDecoratorBuilder AddSimulatation()
        //{
        //    if (this.IsAsync)
        //    {
        //        this.handlers.Add(typeof(SaveSimulateDataDecoratorAsync<,>));
        //    }
        //    else
        //    {
        //        this.handlers.Add(typeof(SaveSimulateDataDecorator<,>));
        //    }

            


        //    return this;
        //}

        public IQueryHandlerDecoratorBuilder AddErrorLogging()
        {

            if (this.IsAsync)
            {
                this.handlers.Add(typeof(ErrorLoggingQueryDecoratorAsync<,>));
            }
            else
            {
                this.handlers.Add(typeof(ErrorLoggingQueryDecorator<,>));
            }
            

            return this;

        }

        public IQueryHandlerDecoratorBuilder AddPerformanceCounter()
        {

            if (this.IsAsync)
            {
                this.handlers.Add(typeof(PerformanceCounterQueryDecoratorAsync<,>));
            }
            else
            {
                this.handlers.Add(typeof(PerformanceCounterQueryDecorator<,>));
            }


            return this;

        }

        public IQueryHandlerDecoratorBuilder AddSearching()
        {
            if (this.IsAsync)
            {
                this.handlers.Add(typeof(ElasticSearchQueryDecoratorAsync<,>));
            }

            return this;
        }

        public IQueryHandlerDecoratorBuilder AddIdentity()
        {
            if (this.IsAsync)
            {
                this.handlers.Add(typeof(IdentityQueryDecoratorAsync<,>));
            }
            else
            {
                this.handlers.Add(typeof(IdentityQueryDecorator<,>));
            }

            return this;
        }

        //public IQueryHandlerDecorator AddCaching()
        //{
        //    this.handlers.Add(typeof(ErrorLoggingQueryDecorator<,>));

        //    if (asyncHandlers != null)
        //    {
        //        this.handlers.Add(typeof(ErrorLoggingQueryDecoratorAsync<,>));
        //    }

        //    return this;

        //}



    }
}
