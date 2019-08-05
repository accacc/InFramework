using FluentValidation;
using IF.Core.Data;
using IF.Core.Interfaces;
using System;

namespace IF.Validation.FluentValidation
{
    public class ValidateCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IValidator<TCommand> validator;

        public ValidateCommandDecorator(ICommandHandler<TCommand> innerCommandHandler, IValidator<TCommand> validator=null)
        {
            if (innerCommandHandler == null) throw new ArgumentNullException(nameof(innerCommandHandler));
            //if (validator == null) throw new ArgumentNullException(nameof(validator));
            this.commandHandler = innerCommandHandler;            
            this.validator = validator;
            
        }

        public void Handle(TCommand command)
        {
            if (this.validator != null)
            {
                validator.ValidateAndThrow(command);
            }

            commandHandler.Handle(command);            
        }
    }


}
