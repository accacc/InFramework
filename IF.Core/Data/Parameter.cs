using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public class Parameter

    {
        public Parameter(string Name)
        {
            this.Name = Name;
            this.Direction = ParamDirection.Input;
        }

        public string Name { get; set; }
        public Type Type { get; set; }

        public ParamDirection Direction { get; set; }

        public int? Size { get; set; }

        public object Value { get; set; }

    }
}
