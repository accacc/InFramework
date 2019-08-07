using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.Core.Interfaces;
using IF.Template.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IF.Template.Api.Controllers
{
    public class UserController : ControllerBase
    {

        private readonly IDispatcher dispatcher;


        public UserController(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }


        [HttpGet]
        [Route("api/User/GetUsers")]
        public async Task<UserListResponse> GetUsers([FromQuery] UserListRequest request)
        {
            var result = await dispatcher.QueryAsync<UserListRequest, UserListResponse>(request);
            return result;
        }
    }
}