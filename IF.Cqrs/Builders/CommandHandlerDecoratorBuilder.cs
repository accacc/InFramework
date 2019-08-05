using IF.Cqrs;
using IF.Core.Handler;
using IF.Core.Validation;
using IF.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using IF.Cqrs.Decorators.Command;

namespace IF.Dependency.Builders
{
    public class CommandHandlerDecoratorBuilder : ICommandHandlerDecoratorBuilder
    {
        private readonly List<Type> handlers;
        private readonly bool IsAsync;
        private readonly IInFrameworkBuilder dependencyInjection;


        public CommandHandlerDecoratorBuilder(List<Type> handlers, IInFrameworkBuilder dependencyInjection, bool IsAsync = false)
        {
            this.handlers = handlers;
            this.IsAsync = IsAsync;
            this.dependencyInjection = dependencyInjection;
        }


        public ICommandHandlerDecoratorBuilder AddPerformanceCounter()
        {

            if (this.IsAsync)
            {
                this.handlers.Add(typeof(PerformanceCounterCommandDecoratorAsync<>));
            }
            else
            {
                this.handlers.Add(typeof(PerformanceCounterCommandDecorator<>));
            }


            return this;

        }

        public ICommandHandlerDecoratorBuilder AddAuditing()
        {
            if (this.IsAsync)
            {
                this.handlers.Add(typeof(SaveAllCommandDataDecoratorAsync<>));
            }
            else
            {

                this.handlers.Add(typeof(SaveAllCommandDataDecorator<>));
            }

            

            return this;
        }

        public ICommandHandlerDecoratorBuilder AddCustomCommandHandler<T>()
        {
            this.handlers.Add(typeof(T));
            return this;
        }

        public ICommandHandlerDecoratorBuilder AddErrorLogging()
        {

            if (this.IsAsync)
            {
                this.handlers.Add(typeof(ErrorLoggingCommandDecoratorAsync<>));
            }
            else
            {
                this.handlers.Add(typeof(ErrorLoggingCommandDecorator<>));

            }
            
            return this;

        }

        public ICommandHandlerDecoratorBuilder AddIdentity()
        {

            if (this.IsAsync)
            {
                this.handlers.Add(typeof(IdentityCommandDecoratorAsync<>));
            }
            else
            {
                this.handlers.Add(typeof(IdentityCommandDecorator<>));

            }

            return this;

        }

        public ICommandHandlerDecoratorBuilder AddDataAnnonationValidation()
        {
            if (this.IsAsync)
            {
                this.handlers.Add(typeof(DataAnnotationValidatorCommandDecoratorAsync<>));
            }
            else
            {
                this.handlers.Add(typeof(DataAnnotationValidatorCommandDecorator<>));

            }

            this.dependencyInjection.RegisterType<DataAnnotationValidator, IDataAnnotationValidator>(DependencyScope.Single);
            

            return this;
        }

       
    }
}
