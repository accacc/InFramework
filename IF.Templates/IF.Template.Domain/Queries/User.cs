using IF.Core.Interfaces;
using IF.Persistence;
using IF.Template.Contract.Queries;
using IF.Template.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Template.Domain.Queries
{
    public class UserListHandler : IQueryHandlerAsync<UserListRequest, UserListResponse>
    {
        private readonly IRepository repository;


        public UserListHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public Task<UserListResponse> HandleAsync(UserListRequest request)
        {
            var user = this.repository.GetQuery<IFUser>()
              .Select(x => new UserDto
              {
                  Id = x.Id,
                  UserName = x.UserName,                 
                  Email = x.Email
              }).ToList();

            return new UserListResponse { Data = user };
        }
    }
}
