using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsOneToManyRequest : BaseRequest
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public Guid SourceId { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public List<string> Numbers { get; set; }

    }
}
