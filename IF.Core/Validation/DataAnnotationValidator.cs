using IF.Core.Exception;
using IF.Core.Handler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IF.Core.Validation
{

    public class DataAnnotationValidator : IDataAnnotationValidator
    {

        public DataAnnotationValidator()
        {
        }


        public void Validate(BaseCommand command)
        {
            var context = new ValidationContext(command, null, null);
            var results = new List<ValidationResult>();

            var IsValid = Validator.TryValidateObject(command, context, results, validateAllProperties: true);

            if (!IsValid)
            {
                throw new DataAnnotationValidationException(results);
            }

        }
    }
}
