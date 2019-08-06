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
    public class UserAddHandler : ICommandHandlerAsync<UserAddCommand>
    {
        private readonly IRepository repository;
        public UserAddHandler(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task HandleAsync(UserAddCommand command)
        {
            var user = new IFUser
            {
                Email = command.Email,
                UserName = command.UserName

            };

            await this.repository.AddAsync(user);
            await this.repository.UnitOfWork.SaveChangesAsync();

        }
    }
}
