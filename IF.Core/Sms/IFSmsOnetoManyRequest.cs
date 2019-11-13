using IF.Core.Data;
using IF.Core.Sms.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public interface ISmsOneToManyBulkModel : ISmsBulkModel
    {
        string Message { get; set; }
    }

    public class IFSmsOneToManyRequest : BaseRequest, ISmsOneToManyBulkModel
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public Guid SourceId { get; set; }

        public IFBulkOperationStatus OperationStatus { get; set; }
        public string CallBackMessageTemplate { get; set; }
        public string CallBackNumberId { get; set; }
        public string CallBackPrefixName { get; set; }
        public string SenderPrefixName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string BulkName { get; set; }


        public List<SmsBatchItemModel> Numbers { get; set; }

    }
}
