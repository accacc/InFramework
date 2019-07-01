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
    public class IdentityCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IIdentityService identityService;


        public IdentityCommandDecorator(ICommandHandler<TCommand> commandHandler, IIdentityService identityService = null)
        {
            this.commandHandler = commandHandler;
            this.identityService = identityService;

        }

        public void Handle(TCommand command)
        {
            this.identityService?.SetUserProfile(command);
            commandHandler.Handle(command);
        }

        
    }
}
