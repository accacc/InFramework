using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Excel
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ExcelMetadataAttribute : Attribute
    {
        public int Order { get; set; }
        public bool Visible { get; set; }
        public string PropertyName { get; set; }

    }
}
