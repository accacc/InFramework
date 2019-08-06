using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Contract.Queries
{
    public class UserListRequest:BaseRequest
    {
    }

    public class UserListResponse:BaseResponse
    {

        public List<UserDto> Data { get; set; }
    }

    public class UserDto

    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }


}
