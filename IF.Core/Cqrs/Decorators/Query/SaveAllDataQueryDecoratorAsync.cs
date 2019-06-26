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


namespace IF.Core.Cqrs.Decorators.Query
{

   
    public class SaveAllDataQueryDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse> where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        private readonly IAuditLogService auditLogService;
        //private readonly IAppSettings appSettings;


        public SaveAllDataQueryDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, IAuditLogService auditLogService)
        {
            this.queryHandler = queryHandler;
            this.auditLogService = auditLogService;
            //this.appSettings = appSettings;

        }

        public async Task<TResponse> HandleAsync(TRequest request)
        {

            if (!(request is IIgnoreAuditRequest))
            {
                //await LogRequest(request).ConfigureAwait(false);
                ThreadPool.QueueUserWorkItem(o=> LogRequest(request).ConfigureAwait(false));

            }

            TResponse response = await queryHandler.HandleAsync(request);

            if (response is IAuditResponse)
            {
                ThreadPool.QueueUserWorkItem(o => LogResponse(request, response).ConfigureAwait(false));
            }

            return response;


        }

        private async Task LogRequest(TRequest request)
        {
            Type retype = typeof(TRequest);
            string rename = retype.Name;

            await this.auditLogService.LogAsync(request, request.UniqueId, DateTime.Now, rename, request.ClientIp, request.ApplicationCode,request.UserId.ToString());
        }

        private async Task LogResponse(TRequest request, TResponse response)
        {
            Type typer = typeof(TResponse);
            string namer = typer.Name;

            await this.auditLogService.LogAsync(response, request.UniqueId, DateTime.Now, namer, request.ClientIp, request.ApplicationCode, request.UserId.ToString()).ConfigureAwait(false);
        }
    }
}
