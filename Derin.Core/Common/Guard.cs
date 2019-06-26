using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Common
{
    public static class Guard
    {
        public static void IsNotNull(object argument)
        {
            if (argument == null)
                throw new ArgumentNullException(nameof(argument));
        }

        public static void IsNotEmpty(string argument)
        {
            if (String.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                throw new ArgumentNullException(nameof(argument));
        }
    }
}
