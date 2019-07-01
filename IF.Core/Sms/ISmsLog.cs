using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public interface ISmsLog:IIFSystemTable
    {
        DateTime Date { get; set; }
        
        bool IsSent { get; set; }

        string Message { get; set; }


        string Number { get; set; }


        string Error { get; set; }

        string IntegrationId { get; set; }
        

        Guid SourceId { get; set; }
    }
}
