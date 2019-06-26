using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IF.Web.Mvc.Exception
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
