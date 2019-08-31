using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public interface IIFBulkOperation
    {

        //int EventId { get; set; }
        string BulkName { get; set; }

        int SplitBy { get; set; }

        long Total { get; set; }
        //string Message { get; set; }

        DateTime CreatedDate { get; set; }

        IFBulkOperationStatus Status { get; set; }

        DateTime UpdatedDate { get; set; }

        int BatchCount { get; set; }

        //string SenderPrefixName { get; set; }

        //string CallBackPrefixName { get; set; }

        //string CallBackMessageTemplate { get; set; }

        //string CallBackNumberId { get; set; }

        //DateTime? StartDate { get; set; }

        //DateTime? EndDate { get; set; }
    }
}
