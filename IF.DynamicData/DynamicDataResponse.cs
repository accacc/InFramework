using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.DynamicData
{
    public class DynamicDataResponse<T>:BaseResponse
    {

        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }

    }
}
