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


    public class ApplicationErrorLog: IApplicationErrorLog
    {

    public     string Channel { get; set; }

        public string ExceptionMessage { get; set; }

        public string Logger { get; set; }

        public string UserId { get; set; }

        public string StackTrace { get; set; }

        public string Message { get; set; }
        public string Level { get; set; }

        public string MachineName { get; set; }

        public string IPAddress { get; set; }

        public DateTime LogDate { get; set; }

        public Guid UniqueId { get; set; }
    }
}
