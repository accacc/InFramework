using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.Core.Interfaces;
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


        //[HttpGet]
        //[Route("api/User/GetUsers")]
        //public PagedListResponse<GetUsersQueryViewModel> GetUsers([FromQuery] GetUsersQuery request)
        //{
        //    var result = _dispatcher.Query<GetUsersQuery, PagedListResponse<GetUsersQueryViewModel>>(request);
        //    return result;
        //}
    }
}