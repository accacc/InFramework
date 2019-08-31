using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFBulkOperation : IIFBulkOperation
    {

        public int EventId { get; set; }

        //public IFormFile File { get; set; }
        public string BulkName { get; set; }

        public int SplitBy { get; set; }

        public long Total { get; set; }
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IFBulkOperationStatus Status { get; set; }

        public int BatchCount { get; set; }


        public string SenderPrefixName { get; set; }

        public string CallBackPrefixName { get; set; }

        public string CallBackMessageTemplate { get; set; }

        public string CallBackNumberId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


    }
}
