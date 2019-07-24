using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public interface ISmsLog:IIFSystemTable
    {
        DateTime Date { get; set; }
        
        bool IsSent { get; set; }

        string Message { get; set; }


        string Number { get; set; }


        string Error { get; set; }

        string IntegrationId { get; set; }
        

        Guid SourceId { get; set; }
    }


    public interface ISmsBulkOneToManyOperation
    {
        string BulkName { get; set; }

        int SplitBy { get; set; }

        long Total { get; set; }
        string Message { get; set; }

        DateTime CreatedDate { get; set; }
        SmsOperationStatus Status { get; set; }

        DateTime UpdatedDate { get; set; }

        int BatchCount { get; set; }

        string SenderPrefixName { get; set; }

        string CallBackPrefixName { get; set; }

        string CallBackMessageTemplate { get; set; }

        string CallBackNumberId { get; set; }

        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }
    }
    public class SmsBulkOneToManyOperation:ISmsBulkOneToManyOperation
    {

        //public IFormFile File { get; set; }
        public string BulkName { get; set; }

        public int SplitBy { get; set; }

        public long Total { get; set; }
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public SmsOperationStatus Status { get; set; }

        public int BatchCount { get; set; }


        public string SenderPrefixName { get; set; }

        public string CallBackPrefixName { get; set; }

        public string CallBackMessageTemplate { get; set; }

        public string CallBackNumberId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


    }

    public interface ISmsBatchResultOneToMany
    {

        int BatchNumber { get; set; }
        string BatchName { get; set; }

        SmsOperationStatus Status { get; set; }

        string ErrorCode { get; set; }

        
        DateTime CreatedDate { get; set; }

        Guid SourceId { get; set; }

        string IntegrationId { get; set; }

        int BatchCount { get; set; }


        DateTime UpdateDate { get; set; } 
    }

    public class SmsBatchResultOneToMany: ISmsBatchResultOneToMany
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

    public enum SmsOperationStatus
    {
        Ready = 0,
        InProgress=1,
        Completed=2,
        Cancelled=3,
        Failed=4

    }
}
