using IF.Core.Configuration;
using IF.Core.Exception;
using IF.Core.Handler;
using IF.Core.Log;
using IF.Core.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Cqrs.Decorators.Command
{
    public class IdentityCommandDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly IIdentityService identityService;
        


        public IdentityCommandDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, IIdentityService identityService)
        {
            this.commandHandler = commandHandler;
            this.identityService = identityService;

        }

        public async Task HandleAsync(TCommand command)
        {


            this.identityService?.SetUserProfile(command);
                await commandHandler.HandleAsync(command);



        }
    }
}
