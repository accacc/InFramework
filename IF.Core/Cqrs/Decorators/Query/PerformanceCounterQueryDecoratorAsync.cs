using IF.Core.Handler;
using IF.Core.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Cqrs.Decorators.Query
{
    public class PerformanceCounterQueryDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {

        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        private readonly IPerformanceLogService performanceLogService;

        public PerformanceCounterQueryDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, IPerformanceLogService performanceLogService)
        {
            this.queryHandler = queryHandler;
            this.performanceLogService = performanceLogService;


        }
        public async Task<TResponse> HandleAsync(TRequest request)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();


            var response = await this.queryHandler.HandleAsync(request);

            stopwatch.Stop();

            //await Log(request, stopwatch).ConfigureAwait(false);

            ThreadPool.QueueUserWorkItem(o => Log(request, stopwatch));

            return response;

        }

        private async Task Log(TRequest request, Stopwatch stopwatch)
        {
            Type type = typeof(TRequest);
            string name = type.Name;

            await this.performanceLogService.LogAsync(DateTime.Now, stopwatch.Elapsed.TotalMilliseconds, name, request.UniqueId);
        }
    }
}
