using IF.Persistence;
using IF.Template.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Template.Persistence.EF.Repository
{
    public interface ITestRepository: IRepository
    {
        Task<List<TestDto>> GetTestList();
        Task<TestDto> GetTest();

        Task UpdateTest(TestDto dto);
        Task AddTest(TestDto dto);


    }
}
