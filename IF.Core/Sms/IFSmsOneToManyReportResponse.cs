using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsOneToManyReportResponse:BaseResponse
    {
        public List<IFSmsNumberResult> Results { get; set; }
    }
}
