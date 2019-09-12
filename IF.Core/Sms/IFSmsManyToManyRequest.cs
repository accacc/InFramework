using IF.Core.Data;
using IF.Core.Sms.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{


    public interface ISmsManyToManyBulkModel : ISmsBulkModel
    {
        string Message { get; set; }
    }

    public class IFSmsManyToManyRequest : BaseRequest, ISmsManyToManyBulkModel
    {
        public string Subject { get; set; }

        public Guid SourceId { get; set; }

        public List<SmsBatchItemModel> Messages { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool Force { get; set; }

        public string SenderPrefixName { get; set; }

        public string CallBackPrefixName { get; set; }

        public string CallBackMessageTemplate { get; set; }

        public string CallBackNumberId { get; set; }

        public string Message { get; set; }
        public IFBulkOperationStatus OperationStatus { get; set; }
        public string BulkName { get; set; }
    }

    //public class IFSmsManyToManyModel
    //{
    //    public string Message { get; set; }

    //    public string Number { get; set; }
    //}



}
