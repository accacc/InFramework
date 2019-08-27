using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class SmsBatchResult : ISmsBatchResult
    {

        public int BatchNumber { get; set; }
        public string BatchName { get; set; }

        public SmsOperationStatus Status { get; set; }

        public string ErrorCode { get; set; }

        public int BatchCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid SourceId { get; set; }

        public string IntegrationId { get; set; }




        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
