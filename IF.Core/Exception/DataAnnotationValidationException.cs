using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IF.Core.Exception
{

    public class DataAnnotationValidationException : System.Exception, ISerializable
    {
        public List<ValidationResult> ValidationResults { get; set; }

        public DataAnnotationValidationException()
        {

        }

        public DataAnnotationValidationException(List<ValidationResult> validationResult)
        {
            this.ValidationResults = validationResult;
        }

        protected DataAnnotationValidationException(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
