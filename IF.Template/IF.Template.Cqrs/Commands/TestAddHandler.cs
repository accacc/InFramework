using IF.Core.Data;
using IF.Template.Contract.Commands;
using IF.Template.Persistence.EF.Repository;
using System.Threading.Tasks;

namespace IF.Template.Cqrs.Commands
{
    public class TestCommandHandler : ICommandHandlerAsync<TestAddCommand>
    {
        private readonly ITestRepository repository;
        public TestCommandHandler(ITestRepository repository)
        {
            this.repository = repository;
        }
        public async Task HandleAsync(TestAddCommand command)
        {
            

        }
    }
}
