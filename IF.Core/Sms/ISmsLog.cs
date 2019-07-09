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
    }
    public class SmsBulkOneToManyOperation:ISmsBulkOneToManyOperation
    {

        //public IFormFile File { get; set; }
        public string BulkName { get; set; }

        public int SplitBy { get; set; }

        public long Total { get; set; }
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }        
        public SmsOperationStatus Status { get; set; }


    }

    public interface ISmsBatchResultOneToMany
    {

        string BulkNumber { get; set; }

        SmsOperationStatus Status { get; set; }

        string ErrorCode { get; set; }

        
        DateTime CreatedDate { get; set; }

        Guid SourceId { get; set; }

        string IntegrationId { get; set; }

        int BulkCount { get; set; }


        DateTime UpdateDate { get; set; } 
    }

    public class SmsBatchResultOneToMany: ISmsBatchResultOneToMany
    {

        
        public string BulkNumber { get; set; }

        public SmsOperationStatus Status { get; set; }

        public string ErrorCode { get; set; }

        public int BulkCount { get; set; }
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
        Cancalled=3,
        Error=4

    }
}
