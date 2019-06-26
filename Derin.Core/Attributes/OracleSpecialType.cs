using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
    public class OracleSpecialTypeAttribute:Attribute
    {

        public OracleSpecialTypeAttribute(string Type)
        {
            this.Type = Type;
        }

        public string Type { get; set; }
    }
}
