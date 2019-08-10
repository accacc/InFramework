using IF.Persistence;
using IF.Template.Contract.Queries;
using IF.Template.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Template.Persistence.EF.Queries
{
    public class TestListDbQuery : ITestListQueryAsync
    {


        private readonly IRepository repository;
        public TestListDbQuery(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<TestListResponse> GetAsync(TestListRequest request)
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

