using IF.Core.Data;
using IF.Template.Contract.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Template.Domain.Queries
{
    public class TestListHandler : IQueryHandlerAsync<TestListRequest, TestListResponse>
    {
        private readonly ITestListQueryAsync listQuery;


        public TestListHandler(ITestListQueryAsync repository)
        {
            this.listQuery = repository;
        }

        public async Task<TestListResponse> HandleAsync(TestListRequest request)
        {
            return await this.listQuery.GetAsync(request);
        }
    }
}