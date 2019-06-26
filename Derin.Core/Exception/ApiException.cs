using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Exception
{
   
    public class WebApiException : System.Exception, ISerializable
    {

        public ErrorCode ErrorCode { get; }

        public WebApiException(ErrorCode errorCode)
            : this(errorCode, string.Empty)
        {

        }

        public WebApiException(ErrorCode errorCode, string message)
            : this(errorCode, message, null)
        {

        }

        public WebApiException(ErrorCode errorCode, string message, System.Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }


        protected WebApiException(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
