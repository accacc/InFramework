using Derin.Core.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{


    public class BaseResponseContract : IBaseResponseContract
    {

        public BaseResponseContract()
        {
            this.IsSuccess = false;
        }
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }

        public bool IsSuccess { get; set; }

        public string SuccessMessage { get; set; }

        public bool IsRecordEmpty { get; set; }

        public long SystemTime { get; set; }

        public string Status { get; set; }

        public List<string> Messages { get; set; }


    }


}







