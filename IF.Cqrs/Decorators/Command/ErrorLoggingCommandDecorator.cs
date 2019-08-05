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

namespace IF.Cqrs.Decorators.Command
{
    public class ErrorLoggingCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly ILogService logger;
        //private readonly IAppSettings appSettings;
        //private readonly IDiagnosticMail diagnosticMail;


        public ErrorLoggingCommandDecorator(ICommandHandler<TCommand> commandHandler, ILogService logger)
        {
            this.commandHandler = commandHandler;
            this.logger = logger;
          //  this.appSettings = appSettings;

        }

        public void Handle(TCommand command)
        {
            try
            {

                commandHandler.Handle(command);

            }
            catch (System.Exception exception)
            {
                //if (!(exception is BusinessException))
                {
                    //Task.Run(() => Log(command, exception)).ConfigureAwait(false);
                    ThreadPool.QueueUserWorkItem(o => Log(command, exception));

                }

                throw;
            }

        }

        private void Log(TCommand command, System.Exception exception)
        {
            this.logger.Error(exception, command.GetType().FullName, "Hata oluştu", command.UserId.ToString(), command.UniqueId, command.ClientIp, command.ApplicationCode);

            exception.Data["IsHandled"] = true;
        }
    }
}
