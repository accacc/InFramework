using IF.Core.Configuration;
using IF.Core.Exception;
using IF.Core.Data;
using IF.Core.Log;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IF.Core.Data;

namespace IF.Cqrs.Decorators.Command
{
    public class ErrorLoggingCommandDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly ILogService logger;
        //private readonly IAppSettings appSettings;


        public ErrorLoggingCommandDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, ILogService logger)
        {
            this.commandHandler = commandHandler;
            this.logger = logger;
            //this.appSettings = appSettings;
            //this.diagnosticMail = diagnosticMail;

        }

        public async Task HandleAsync(TCommand command)
        {
            try
            {

                await commandHandler.HandleAsync(command);

            }
            catch (System.Exception exception)
            {

                //if (appSettings.SendMailOnError)
                //{
                //    this.diagnosticMail.SendMail(exception, command, command.UserName);
                //}


                //if (!(exception is BusinessException))
                {
                    //await this.logger.ErrorAsync(exception, command.GetType().FullName, "Hata oluştu", command.UserId.ToString(), command.UniqueId,command.ClientIp,command.ApplicationCode).ConfigureAwait(false);
                    ThreadPool.QueueUserWorkItem(o => this.logger.ErrorAsync(exception, command.GetType().FullName, "Hata oluştu", command.UserId.ToString(), command.UniqueId, command.ClientIp, command.ApplicationCode));
                    exception.Data["IsHandled"] = true;
                }


                throw;

            }



        }
    }
}
