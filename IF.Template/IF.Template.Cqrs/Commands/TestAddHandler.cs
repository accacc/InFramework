using IF.Core.Data;
using IF.Template.Contract.Commands;
using System.Threading.Tasks;

namespace IF.Template.Cqrs.Commands
{
    public class TestCommandHandler : ICommandHandlerAsync<TestAddCommand>
    {
        private readonly ITestAddDbCommandAsync dbCommand;
        public TestCommandHandler(ITestAddDbCommandAsync dbCommand)
        {
            this.dbCommand = dbCommand;
        }
        public async Task HandleAsync(TestAddCommand command)
        {
            await this.dbCommand.ExecuteAsync(command);

        }
    }
}
