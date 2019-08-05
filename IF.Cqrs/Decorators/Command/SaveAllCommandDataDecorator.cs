using IF.Core.Configuration;
using IF.Core.Handler;
using IF.Core.Json;
using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Command
{
    public class SaveAllCommandDataDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IAuditLogService auditLogService;
        //private readonly IAppSettings appSettings;

        public SaveAllCommandDataDecorator(ICommandHandler<TCommand> commandHandler, IAuditLogService auditLogService)
        {
            this.commandHandler = commandHandler;
            this.auditLogService = auditLogService;
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
