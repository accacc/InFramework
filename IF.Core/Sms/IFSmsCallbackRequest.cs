using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsCallbackRequest:BaseRequest
    {
        public string Prefix { get; set; }

        public string Status { get; set; }
    }
}
