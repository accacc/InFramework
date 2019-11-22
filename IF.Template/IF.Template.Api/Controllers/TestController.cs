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
        [Route("api/Resource/TestList")]
        public async Task<IActionResult> TestList([FromQuery] TestListRequest request)
        {
            var response = await dispatcher.QueryAsync<TestListRequest, TestListResponse>(new TestListRequest());
            return Ok(response);

        }
    }
}