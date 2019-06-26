using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Excel
{
    public class ExcelItemValidator
    {

    }

    public abstract class ExcelValidator<T>
    {

        public List<string> Errors { get; }
        public List<ExcelValidationError> ModelErrors { get; }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        public void AddError(ExcelValidationError error)
        {
            ModelErrors.Add(error);
        }

        public abstract void Validate(T @object);




    }
}
