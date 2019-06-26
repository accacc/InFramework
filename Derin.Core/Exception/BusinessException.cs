using System.Runtime.Serialization;

namespace Derin.Core.Exception
{
    public class BusinessException : System.Exception, ISerializable
    {

        public BusinessException()
        {
        }
        public BusinessException(string message):base(message)
        {
            
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
