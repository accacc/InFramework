using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Derin.Core.Mvc.Exception
{
    public class ModelStateException : System.Exception, ISerializable
    {
        public ModelStateDictionary ModelStateDictionary { get; set; }

        public ModelStateException()
        {

        }

        public ModelStateException(ModelStateDictionary modelStateDictionary)
        {
            this.ModelStateDictionary = modelStateDictionary;
        }

        protected ModelStateException(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}
