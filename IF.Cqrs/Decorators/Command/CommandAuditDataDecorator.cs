using IF.Core.Data;
using IF.Core.Log;

using System;
using System.Threading;

namespace IF.Cqrs.Decorators.Command
{
    public class CommandAuditDataDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly ICommandAuditDataService auditLogService;
        //private readonly IAppSettings appSettings;

        public CommandAuditDataDecorator(ICommandHandler<TCommand> commandHandler, ICommandAuditDataService commandAuditDataService)
        {
            this.commandHandler = commandHandler;
            this.auditLogService = commandAuditDataService;
            //this.appSettings = appSettings;

        }

        public void Handle(TCommand command)
        {
            //Task.Run(() => Log(command)).ConfigureAwait(false);

            ThreadPool.QueueUserWorkItem(o => Log(command));

            commandHandler.Handle(command);

        }

        private void Log(TCommand command)
        {
            if (!(command is IIgnoreAuditCommand))
            {
                Type type = typeof(TCommand);

                string name = type.Name;

                this.auditLogService.Log(command, command.UniqueId, DateTime.Now, name, command.ClientIp, command.ApplicationCode, command.UserId.ToString());

            }
        }
    }
}
