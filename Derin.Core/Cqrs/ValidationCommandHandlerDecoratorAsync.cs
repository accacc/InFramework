using Derin.Core.Handler;
using Derin.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Cqrs
{

    public class ValidationCommandHandlerDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly IValidator validator;
        private readonly ICommandHandlerAsync<TCommand> handler;

        public ValidationCommandHandlerDecoratorAsync(IValidator validator, ICommandHandlerAsync<TCommand> handler)
        {
            this.validator = validator;
            this.handler = handler;
        }

        public async Task HandleAsync(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.validator.ValidateObject(command);

            await this.handler.HandleAsync(command);
        }
    }
}
