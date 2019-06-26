using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Extensions
{
    public static class BooleanExtensions
    {
        public static int? ConvertToInt(this bool? value)
        {
            return value == null ? (int?) null : value==true ? 1 : 0;
        }
        public static int? ConvertToInt(this bool value)
        {
            return value ? 1 : 0;
        }
    }
}
