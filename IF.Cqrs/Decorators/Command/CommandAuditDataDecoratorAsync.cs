using IF.Core.Data;
using IF.Core.Log;

using System;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Command
{
    public class CommandAuditDataDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly ICommandAuditDataService commandAuditDataService;
        //private readonly IAppSettings appSettings;

        public CommandAuditDataDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, ICommandAuditDataService commandAuditDataService)
        {
            this.commandHandler = commandHandler;
            this.commandAuditDataService = commandAuditDataService;
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

                await this.commandAuditDataService.LogAsync(command, command.UniqueId, DateTime.Now, name, command.ClientIp, command.ApplicationCode,command.UserId.ToString());
            }
        }
    }
}
