using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Email
{
    public interface IEmailLog:IIFSystemTable
    {
        string From { get; set; }
        string To { get; set; }
        string Body { get; set; }


        DateTime Date { get; set; }
        string Type { get; set; }
        bool IsSent { get; set; }
        string Subject { get; set; }

        Guid UniqueId { get; set; }

        Guid SourceId { get; set; }
    }
}
