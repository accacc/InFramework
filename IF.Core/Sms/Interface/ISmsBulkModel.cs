using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms.Interface
{
    public interface ISmsBulkModel
    {
        //string Message { get; set; }
        IFBulkOperationStatus OperationStatus { get; set; }
        string CallBackMessageTemplate { get; set; }
        string CallBackNumberId { get; set; }
        string CallBackPrefixName { get; set; }
        string SenderPrefixName { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        string BulkName { get; set; }



    }
}
