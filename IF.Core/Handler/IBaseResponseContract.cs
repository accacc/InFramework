using IF.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Handler
{
    public interface IBaseResponseContract
    {

         int ExecutionTime { get; set; }

        string ErrorCode { get; set; }

        string ErrorMessage { get; set; }
        string ErrorDetail { get; set; }

        bool IsSuccess { get; set; }

        string SuccessMessage { get; set; }

        bool IsRecordEmpty { get; set; }

        long SystemTime { get; set; }

        ExceptionTypes ExceptionType { get; set; }

        string Status { get; set; }

        List<string> Messages { get; set; }
    }
}
