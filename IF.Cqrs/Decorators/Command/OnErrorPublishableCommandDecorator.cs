using IF.Core.Data;
using IF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Command
{
    public class OnErrorPublishableCommandDecorator<TCommand> : IOnErrorPublishableCommandAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly IOnErrorPublishableCommandAsync<TCommand> innerCommandHandler;
        public OnErrorPublishableCommandDecorator(IOnErrorPublishableCommandAsync<TCommand> innerCommandHandler)
        {
            if (innerCommandHandler == null) throw new ArgumentNullException(nameof(innerCommandHandler));

            this.innerCommandHandler = innerCommandHandler;

        }

        public async Task HandleAsync(TCommand command)
        {
            try
            {
                await this.innerCommandHandler.HandleAsync(command);
            }
            catch
            {
                await this.PublishAsync(command);

            }


        }

        public async Task PublishAsync(TCommand command)
        {
            await this.innerCommandHandler.PublishAsync(command);
        }
    }
}
