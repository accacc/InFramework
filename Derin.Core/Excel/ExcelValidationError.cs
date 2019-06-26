using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Excel
{
    public class ExcelValidationError
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
