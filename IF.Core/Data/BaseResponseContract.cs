using IF.Core.Exception;
using System;
using System.Collections.Generic;

namespace IF.Core.Handler
{
    public class BaseResponseContract : IBaseResponseContract
    {

        public BaseResponseContract()
        {
            this.IsSuccess = true;
        }
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }

        public bool IsSuccess { get; set; }

        public string SuccessMessage { get; set; }

        public bool IsRecordEmpty { get; set; }

        public long SystemTime { get; set; }

        public ExceptionTypes ExceptionType { get; set; }

        public string Status { get; set; }

        public string Code { get; set; }

        public List<string> Messages { get; set; }

        public int ExecutionTime { get; set; }

        public void FromException(System.Exception exception)
        {
            this.ErrorMessage = exception.Message;

            while (exception.InnerException != null)
                exception = exception.InnerException;

            this.ErrorDetail = exception.Message;

            this.IsSuccess = false;           
            

        }

        public void FromBusinessException(BusinessException exception)
        {
            this.ErrorMessage = exception.Message;            
            this.ErrorDetail = exception.Message;
            this.ErrorCode = exception.ErrorCode;
            this.IsSuccess = false;


        }

        public void FromDataAnnotationValidationException(DataAnnotationValidationException exception)
        {
            this.IsSuccess = false;
            this.Messages = new List<string>();

            foreach (var result in exception.ValidationResults)
            {
                this.Messages.Add(String.Join(",", result.MemberNames) + " : " + result.ErrorMessage);
            }


        }


    }


}







