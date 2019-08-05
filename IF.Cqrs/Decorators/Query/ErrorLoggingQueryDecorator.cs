using IF.Core.Configuration;
using IF.Core.Exception;
using IF.Core.Data;
using IF.Core.Log;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Query
{
    public class ErrorLoggingQueryDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {

        private readonly ILogService logger;
        private readonly IQueryHandler<TRequest, TResponse> queryHandler;

        public ErrorLoggingQueryDecorator(IQueryHandler<TRequest, TResponse> queryHandler, ILogService logger)
        {
            this.logger = logger;
            this.queryHandler = queryHandler;

        }
        public TResponse Handle(TRequest request)
        {
            try
            {

                return queryHandler.Handle(request);

            }
            catch (System.Exception exception)
            {
                // Task.Run(() => Log(request, exception)).ConfigureAwait(false);
                ThreadPool.QueueUserWorkItem(o => Log(request, exception));

                throw;
            }

        }

        private void Log(TRequest request, System.Exception exception)
        {
            //if (!(exception is BusinessException))


            this.logger.Error(exception, request.GetType().FullName, "Hata oluştu", request.UserId.ToString(), request.UniqueId, request.ClientIp, request.ApplicationCode);

            exception.Data["IsHandled"] = true;
        }
    }
}
