using IF.Core.Data;
using IF.Core.Validation;
using System;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Command
{

    public class DataAnnotationValidatorCommandDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly IDataAnnotationValidator validator;
        private readonly ICommandHandlerAsync<TCommand> handler;

        public DataAnnotationValidatorCommandDecoratorAsync(ICommandHandlerAsync<TCommand> handler, IDataAnnotationValidator validator)
        {
            this.validator = validator;
            this.handler = handler;
        }

        public async Task HandleAsync(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.validator.Validate(command);

            await this.handler.HandleAsync(command);
        }
    }
}
