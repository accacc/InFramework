using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsCallbackResponse:BaseResponse
    {
        public List<IFSmsCallbackXmlMessages> List { get; set; }
    }
}
