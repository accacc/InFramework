using IF.Core.Data;
using IF.NetCore.Scheduler;
using IF.Template.Contract.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IF.Template.Scheduler.Scheduler
{
    public class TestScheduler : ScheduledProcessor
    {
     
        private readonly IDispatcher dispatcher;
        public TestScheduler(IServiceScopeFactory serviceScopeFactory, IDispatcher dispatcher) : base(serviceScopeFactory)
        {
            this.dispatcher = dispatcher;
        }
        protected override string Schedule => "* * * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            try
            {
                await this.dispatcher.CommandAsync(new TestAddCommand());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
