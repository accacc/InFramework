using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsOneToManyReportRequest:BaseRequest
    {
        public string IntegrationId { get; set; }
    }
}
