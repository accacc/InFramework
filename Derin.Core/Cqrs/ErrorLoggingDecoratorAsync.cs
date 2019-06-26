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

  
    public class ErrorLoggingCommandDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly ILogService logger;
        private readonly IAppSettings appSettings;
        private readonly IDiagnosticMail diagnosticMail;


        public ErrorLoggingCommandDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, ILogService logger,IAppSettings appSettings, IDiagnosticMail diagnosticMail)
        {
            this.commandHandler = commandHandler;
            this.logger = logger;
            this.appSettings = appSettings;
            this.diagnosticMail = diagnosticMail;
            
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

                //this.logger.Error(exception);

                //exception.Data["IsHandled"] = true;

                throw;
            }

        }
    }

    public class ErrorLoggingQueryDecoratorAsync<TRequest, TResponse> :IQueryHandlerAsync<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {

        private readonly ILogService logger;
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        private readonly IAppSettings appSettings;
        private readonly IDiagnosticMail diagnosticMail;

        public ErrorLoggingQueryDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler,ILogService logger, IAppSettings appSettings, IDiagnosticMail diagnosticMail)
        {
            this.logger = logger;
            this.queryHandler = queryHandler;
            this.appSettings = appSettings;
            this.diagnosticMail = diagnosticMail;

        }
        public async Task<TResponse> HandleAsync(TRequest request)
        {
            try
            {

                return await queryHandler.HandleAsync(request);

            }
            catch (System.Exception exception)
            {


                //if (appSettings.SendMailOnError)
                //{
                //    this.diagnosticMail.SendMail(exception, request, request.UserName);
                //}

                //this.logger.Error(exception);

                //exception.Data["IsHandled"] = true;

                throw;
            }

        }
    }

  


   



}
