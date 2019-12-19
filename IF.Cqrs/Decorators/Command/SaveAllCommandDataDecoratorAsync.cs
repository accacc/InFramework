using IF.Core.Configuration;
using IF.Core.Data;
using IF.Core.Data;
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
    public class SaveAllCommandDataDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly IAuditLogService auditLogService;
        //private readonly IAppSettings appSettings;

        public SaveAllCommandDataDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, IAuditLogService auditLogService)
        {
            this.commandHandler = commandHandler;
            this.auditLogService = auditLogService;
            //this.appSettings = appSettings;

        }

        public async Task HandleAsync(TCommand command)
        {
            await Log(command);

            //ThreadPool.QueueUserWorkItem(o=>Log(command));

            await commandHandler.HandleAsync(command);


        }

        private async Task Log(TCommand command)
        {
            if (!(command is IIgnoreAuditCommand))
            {


                Type type = typeof(TCommand);

                string name = type.Name;

                await this.auditLogService.LogAsync(command, command.UniqueId, DateTime.Now, name, command.ClientIp, command.ApplicationCode,command.UserId.ToString());
            }
        }
    }
}
