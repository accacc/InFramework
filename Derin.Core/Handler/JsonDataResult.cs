using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    public class JsonDataResult : BaseResponseContract
    {
        public object Data { get; set; }
    }
}
