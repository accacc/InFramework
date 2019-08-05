using IF.Core.Handler;
using IF.Core.Performance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Cqrs.Decorators.Command
{
    public class PerformanceCounterCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IPerformanceLogService performanceLogService;

        public PerformanceCounterCommandDecorator(ICommandHandler<TCommand> commandHandler, IPerformanceLogService performanceLogService)
        {
            this.commandHandler = commandHandler;
            this.performanceLogService = performanceLogService;
        }

        public void Handle(TCommand command)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            commandHandler.Handle(command);

            stopwatch.Stop();

            //Task.Run(() => Log(command, stopwatch)).ConfigureAwait(false);

            ThreadPool.QueueUserWorkItem(o => Log(command, stopwatch));
        }

        private void Log(TCommand command, Stopwatch stopwatch)
        {
            Type type = typeof(TCommand);

            string name = type.Name;

            this.performanceLogService.Log(DateTime.Now, stopwatch.Elapsed.Milliseconds, name, command.UniqueId);
        }
    }
}
