using IF.Core.Data;
using IF.Template.Contract.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IF.Template.Api.Controllers
{
    public class TestController : ControllerBase
    {

        private readonly IDispatcher dispatcher;


        public TestController(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }


        [HttpGet]
        [Route("api/Test/Get")]
        public async Task<TestListResponse> Get([FromQuery] TestListRequest request)
        {
            var result = await dispatcher.QueryAsync<TestListRequest, TestListResponse>(request);
            return result;
        }

        [HttpPost]
        [Route("api/Test/Add")]
        public async Task<TestListResponse> Add([FromQuery] TestListRequest request)
        {
            var result = await dispatcher.QueryAsync<TestListRequest, TestListResponse>(request);
            return result;
        }
    }
}