using IF.Core.Data;
using IF.Core.Data;
using IF.Core.Validation;
using System;

namespace IF.Cqrs.Decorators.Command
{
    public class DataAnnotationValidatorCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly IDataAnnotationValidator validator;
        private readonly ICommandHandler<TCommand> handler;

        public DataAnnotationValidatorCommandDecorator(ICommandHandler<TCommand> handler, IDataAnnotationValidator validator)
        {
            this.validator = validator;
            this.handler = handler;
        }

        void ICommandHandler<TCommand>.Handle(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.validator.Validate(command);

            this.handler.Handle(command);
        }
    }

}
