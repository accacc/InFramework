using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Validation
{
    public class DataAnnotationsValidator : IValidator
    {

        public DataAnnotationsValidator()
        {
        }

        [DebuggerStepThrough]
        void IValidator.ValidateObject(object instance)
        {
            var context = new ValidationContext(instance, null, null);
            Validator.ValidateObject(instance, context, validateAllProperties: true);
        }
    }
}
