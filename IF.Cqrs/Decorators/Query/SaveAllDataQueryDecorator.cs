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
    public class SaveAllDataQueryDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse> where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
        private readonly IAuditLogService auditLogService;
        //private readonly IAppSettings appSettings;


        public SaveAllDataQueryDecorator(IQueryHandler<TRequest, TResponse> queryHandler, IAuditLogService auditLogService)
        {
            this.queryHandler = queryHandler;
            this.auditLogService = auditLogService;
            //this.appSettings = appSettings;

        }

        public TResponse Handle(TRequest request)
        {

            if (!(request is IIgnoreAuditRequest))
            {
                Task.Run(() => LogRequest(request)).ConfigureAwait(false);

            }

            TResponse response = queryHandler.Handle(request);


            if (request is IAuditResponse)
            {
                //Task.Run(() => LOgResponnse(request, response)).ConfigureAwait(false);
                ThreadPool.QueueUserWorkItem(o => LogResponnse(request, response));
            }

            return response;

        }

        private void LogResponnse(TRequest request, TResponse response)
        {
            Type rstype = typeof(TResponse);
            string rsname = rstype.Name;

            this.auditLogService.Log(response, request.UniqueId, DateTime.Now, rsname, request.ClientIp, request.ApplicationCode, request.UserId.ToString());
        }

        private void LogRequest(TRequest request)
        {
            Type rtype = typeof(TRequest);
            string rname = rtype.Name;

            this.auditLogService.Log(request, request.UniqueId, DateTime.Now, rname, request.ClientIp, request.ApplicationCode, request.UserId.ToString());
        }
    }
}
