using IF.Persistence;
using IF.Template.Contract.Commands;
using IF.Template.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Template.Persistence.EF.Commands
{
    public class TestAddDbCommand : ITestAddDbCommandAsync
    {

        private readonly IRepository repository;
        public TestAddDbCommand(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task ExecuteAsync(TestAddCommand command)
        {
            var data = new TestEntity
            {
                Desc = command.Description,
                Name = command.Name

            };

            await this.repository.AddAsync(data);
            await this.repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
