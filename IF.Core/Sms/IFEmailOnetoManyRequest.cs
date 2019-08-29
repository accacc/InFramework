using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFEmailOneToManyRequest : BaseRequest
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public Guid SourceId { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public List<string> Emails { get; set; }

        public int EventId { get; set; }

    }
}
