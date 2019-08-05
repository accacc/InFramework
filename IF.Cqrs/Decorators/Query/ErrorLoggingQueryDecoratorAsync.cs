using IF.Core.Configuration;
using IF.Core.Exception;
using IF.Core.Handler;
using IF.Core.Log;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Query
{
    public class ErrorLoggingQueryDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {

        private readonly ILogService logger;
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        

        public ErrorLoggingQueryDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, ILogService logger)
        {
            this.logger = logger;
            this.queryHandler = queryHandler;
        

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


                //if (!(exception is BusinessException))
                {
                    //await this.logger.ErrorAsync(exception, request.GetType().FullName, "Hata oluştu", request.UserId.ToString(), request.UniqueId,request.ClientIp,request.ApplicationCode).ConfigureAwait(false);

                    ThreadPool.QueueUserWorkItem(o => this.logger.ErrorAsync(exception, request.GetType().FullName, "Hata oluştu", request.UserId.ToString(), request.UniqueId, request.ClientIp, request.ApplicationCode));
                }

                exception.Data["IsHandled"] = true;

                throw;
            }

        }
    }
}
