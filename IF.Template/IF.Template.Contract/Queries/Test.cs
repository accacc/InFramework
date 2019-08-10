using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Contract.Queries
{
    public class TestListRequest:BaseRequest
    {
    }

    public class TestListResponse:BaseResponse
    {

        public List<TestDto> Data { get; set; }
    }

    public class TestDto

    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }


}
