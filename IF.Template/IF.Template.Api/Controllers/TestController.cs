using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.Core.Interfaces;
using IF.Template.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<TestListResponse> GetUsers([FromQuery] TestListRequest request)
        {
            var result = await dispatcher.QueryAsync<TestListRequest, TestListResponse>(request);
            return result;
        }
    }
}