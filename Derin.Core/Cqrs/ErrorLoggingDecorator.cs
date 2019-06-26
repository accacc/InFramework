using Derin.Core.Configuration;
using Derin.Core.Exception;
using Derin.Core.Handler;
using Derin.Core.Log;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Cqrs
{

  
    public class ErrorLoggingCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly ILogService logger;
        private readonly IAppSettings appSettings;
        private readonly IDiagnosticMail diagnosticMail;


        public ErrorLoggingCommandDecorator(ICommandHandler<TCommand> commandHandler, ILogService logger,IAppSettings appSettings, IDiagnosticMail diagnosticMail)
        {
            this.commandHandler = commandHandler;
            this.logger = logger;
            this.appSettings = appSettings;
            this.diagnosticMail = diagnosticMail;
            
        }

        public void Handle(TCommand command)
        {
            try
            {

                commandHandler.Handle(command);

            }
            catch (System.Exception exception)
            {

                if (appSettings.SendMailOnError)
                {
                    this.diagnosticMail.SendMail(exception, command, command.UserName);
                }

                this.logger.Error(exception);

                exception.Data["IsHandled"] = true;

                throw;
            }

        }
    }

    public class ErrorLoggingQueryDecorator<TRequest, TResponse> :IQueryHandler<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {

        private readonly ILogService logger;
        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
        private readonly IAppSettings appSettings;
        private readonly IDiagnosticMail diagnosticMail;

        public ErrorLoggingQueryDecorator(IQueryHandler<TRequest, TResponse> queryHandler,ILogService logger, IAppSettings appSettings, IDiagnosticMail diagnosticMail)
        {
            this.logger = logger;
            this.queryHandler = queryHandler;
            this.appSettings = appSettings;
            this.diagnosticMail = diagnosticMail;

        }
        public TResponse Handle(TRequest request)
        {
            try
            {

                return queryHandler.Handle(request);

            }
            catch (System.Exception exception)
            {


                if (appSettings.SendMailOnError)
                {
                    this.diagnosticMail.SendMail(exception, request, request.UserName);
                }

                this.logger.Error(exception);

                exception.Data["IsHandled"] = true;

                throw;
            }

        }
    }

  


   



}
