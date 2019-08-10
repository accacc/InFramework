using IF.Core.Data;
using IF.Core.Data;
using IF.Core.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Command
{
    public class PerformanceCounterCommandDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly IPerformanceLogService performanceLogService;

        public PerformanceCounterCommandDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, IPerformanceLogService performanceLogService)
        {
            this.commandHandler = commandHandler;
            this.performanceLogService = performanceLogService;
        }

        public async Task HandleAsync(TCommand command)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            await commandHandler.HandleAsync(command);

            stopwatch.Stop();

           await Log(command, stopwatch);

         //   ThreadPool.QueueUserWorkItem(o => Log(command, stopwatch));
        }

        private async Task Log(TCommand command, Stopwatch stopwatch)
        {           

            string name = nameof(TCommand);

            await this.performanceLogService.LogAsync(DateTime.Now, stopwatch.Elapsed.Milliseconds, name, command.UniqueId);
        }
    }
}
