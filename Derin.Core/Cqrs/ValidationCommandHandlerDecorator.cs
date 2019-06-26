using Derin.Core.Handler;
using Derin.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Cqrs
{
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly IValidator validator;
        private readonly ICommandHandler<TCommand> handler;

        public ValidationCommandHandlerDecorator(IValidator validator, ICommandHandler<TCommand> handler)
        {
            this.validator = validator;
            this.handler = handler;
        }

        void ICommandHandler<TCommand>.Handle(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.validator.ValidateObject(command);

            this.handler.Handle(command);
        }
    }

}
