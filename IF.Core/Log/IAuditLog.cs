using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Log
{
    public interface IIFSystemTable
    {
        Guid UniqueId { get; set; }
    }

    public interface IAuditLog:IIFSystemTable
    {
        

        DateTime LogDate { get; set; }


        string ObjectName { get; set; }


        string JsonObject { get; set; }

        string UserId { get; set; }

        string Channel { get; set; }

        string ClientId { get; set; }
    }


    public class AuditLog : IAuditLog
    {


        public Guid UniqueId { get; set; }

        public DateTime LogDate { get; set; }


        public string ObjectName { get; set; }


        public string JsonObject { get; set; }

        public string UserId { get; set; }

        public string Channel { get; set; }

        public string ClientId { get; set; }
    }
}
