using System.Runtime.Serialization;

namespace IF.Core.Exception
{
    public class BusinessException : System.Exception, ISerializable
    {
        public string ErrorCode { get; set; }
        public BusinessException()
        {
        }
        public BusinessException(string message,string errorCode = ""):base(message)
        {
            this.ErrorCode = errorCode;
        }
        public BusinessException(string message, System.Exception inner)
            : base(message, inner)
        {
            
        }

        
        protected BusinessException(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}
