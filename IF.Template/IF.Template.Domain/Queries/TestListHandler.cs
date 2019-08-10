using IF.Core.Interfaces;
using IF.Persistence;
using IF.Template.Contract.Queries;
using IF.Template.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Template.Domain.Queries
{
    public class TestListHandler : IQueryHandlerAsync<TestListRequest, TestListResponse>
    {
        private readonly IRepository repository;


        public TestListHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TestListResponse> HandleAsync(TestListRequest request)
        {
            var data = await this.repository.GetQuery<TestEntity>()
              .Select(x => new TestDto
              {
                  Id = x.Id,
                  Name = x.Name,                 
                  Description = x.Desc
              }).ToListAsync();

            return new TestListResponse { Data = data };
        }
    }
}
