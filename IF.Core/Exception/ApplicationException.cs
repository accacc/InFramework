using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IF.Core.Exception
{

    public class IFApplicationException : System.Exception, ISerializable
    {
        public string ApplicationCode { get; set; }
        public IFApplicationException()
        {
        }
        public IFApplicationException(string message, string ApplicationCode = "") : base(message)
        {
            this.ApplicationCode = ApplicationCode;
        }
        public IFApplicationException(string message, System.Exception inner)
            : base(message, inner)
        {

        }


        protected IFApplicationException(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
