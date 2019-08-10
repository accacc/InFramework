using IF.Core.Data;
using IF.Core.Interfaces;
using IF.Persistence;
using IF.Template.Contract.Commands;
using IF.Template.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Template.Domain.Commands
{
    public class TestCommandHandler : ICommandHandlerAsync<TestAddCommand>
    {
        private readonly IRepository repository;
        public TestCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task HandleAsync(TestAddCommand command)
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
