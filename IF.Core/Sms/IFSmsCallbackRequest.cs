using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsCallbackRequest:BaseRequest
    {
        public string Prefix { get; set; }

        public string MessageStatus { get; set; }
    }
}
