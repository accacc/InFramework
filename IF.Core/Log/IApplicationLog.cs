using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Log
{
    public interface IApplicationErrorLog:IIFSystemTable
    {

        string Channel { get; set; }

        string ExceptionMessage { get; set; }

        string Logger { get; set; }

        string UserId { get; set; }

        string StackTrace { get; set; }

        string Message { get; set; }
        string Level { get; set; }

        string MachineName { get; set; }

        string IPAddress { get; set; }

        DateTime LogDate { get; set; }
    }
}
