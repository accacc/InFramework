﻿using FluentValidation;
using IF.Core.Data;
using IF.Core.Data;
using System;
using System.Threading.Tasks;

namespace IF.Validation.FluentValidation
{


    public class ValidateCommandDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> innerCommandHandler;
        private readonly IValidator<TCommand> validator;

        public ValidateCommandDecoratorAsync(ICommandHandlerAsync<TCommand> innerCommandHandler, IValidator<TCommand> validator = null)
        {
            if (innerCommandHandler == null) throw new ArgumentNullException(nameof(innerCommandHandler));
            this.innerCommandHandler = innerCommandHandler;
            this.validator = validator;

        }

        public async Task HandleAsync(TCommand command)
        {
            if (this.validator != null)
            {
                validator.ValidateAndThrow(command);
            }

            await innerCommandHandler.HandleAsync(command);
        }
    }
}
