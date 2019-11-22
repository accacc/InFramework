using IF.Core.Data;
using IF.Template.Contract.Queries;
using IF.Template.Persistence.EF.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Template.Cqrs.Queries
{
    public class TestListHandler : IQueryHandlerAsync<TestListRequest, TestListResponse>
    {
        private readonly ITestRepository testRepository;


        public TestListHandler(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public async Task<TestListResponse> HandleAsync(TestListRequest request)
        {
            TestListResponse response = new TestListResponse();

            response.Data =  await this.testRepository.GetTestList();

            return response;
        }
    }
}