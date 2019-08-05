using IF.Core.Data;
using IF.Core.Interfaces;
using IF.Core.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Query
{
    public class PerformanceCounterQueryDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {

        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
        private readonly IPerformanceLogService performanceLogService;
        public PerformanceCounterQueryDecorator(IQueryHandler<TRequest, TResponse> queryHandler, IPerformanceLogService performanceLogService)
        {
            this.queryHandler = queryHandler;
            this.performanceLogService = performanceLogService;


        }
        public TResponse Handle(TRequest request)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();


            var response = this.queryHandler.Handle(request);

            stopwatch.Stop();

            //Task.Run(() => LOg(request, stopwatch)).ConfigureAwait(false);
            ThreadPool.QueueUserWorkItem(o => LOg(request, stopwatch));

            return response;

        }

        private void LOg(TRequest request, Stopwatch stopwatch)
        {
            Type type = typeof(TRequest);
            string name = type.Name;


            this.performanceLogService.Log(DateTime.Now, stopwatch.Elapsed.TotalMilliseconds, name, request.UniqueId);
        }
    }
}
