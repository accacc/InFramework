using IF.Persistence.EF;
using IF.Template.Contract.Models;
using IF.Template.Contract.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Template.Persistence.EF.Repository
{
    public class TestRepository : GenericRepository, ITestRepository
    {
        public TestRepository(TestDbContext dbContext) : base(dbContext)
        {

        }

        public Task AddTest(TestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<TestDto> GetTest()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestDto>> GetTestList()
        {
            return await GetQuery<TestEntity>().Select(s => new TestDto
            {
                Description = s.Desc,
                Id = s.Id,
                Name = s.Name
                
            }).OrderBy(r => r.Name)
             .ToListAsync();
        }

        public Task UpdateTest(TestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
