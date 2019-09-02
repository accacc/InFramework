using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{

    public interface ISmsBulkModel
    {
        string Message { get; set; }
        IFBulkOperationStatus OperationStatus { get; set; }
        string CallBackMessageTemplate { get; set; }
        string CallBackNumberId { get; set; }
        string CallBackPrefixName { get; set; }
        string SenderPrefixName { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        string BulkName { get; set; }



    }
    public class IFSmsOneToManyRequest : BaseRequest, ISmsBulkModel
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public Guid SourceId { get; set; }

        public IFBulkOperationStatus OperationStatus { get; set; }
        public string CallBackMessageTemplate { get; set; }
        public string CallBackNumberId { get; set; }
        public string CallBackPrefixName { get; set; }
        public string SenderPrefixName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string BulkName { get; set; }


        public List<SmsBatchItemModel> Numbers { get; set; }

    }
}
