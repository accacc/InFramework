using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using IF.Web.Mvc.Model;

namespace IF.Web.Mvc.Exception
{
    public class ModelStateException : System.Exception, ISerializable
    {
        public ModelStateDictionary ModelStateDictionary { get; set; }

        public ModelStateException()
        {

        }

        public ModelStateException(ModelStateDictionary modelStateDictionary):base(modelStateDictionary.ModelStateErrors())
        {
            this.ModelStateDictionary = modelStateDictionary;
        }

        protected ModelStateException(SerializationInfo info, StreamingContext context)
        {
           
        }
    }
}
