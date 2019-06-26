using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Convert
{
    public static class ConvertHelper
    {

        public static T ConvertValue<T>(object value)
        {
            try
            {
                if (value is IConvertible)
                    return (T)System.Convert.ChangeType(value, typeof(T), new CultureInfo("en-US"));
                return (T)System.Convert.ChangeType(value, typeof(T));
            }

            catch (System.Exception exp)
            {
                throw new System.Exception("Conversion process fail : " + exp.Message);
            }
        }
    }
}
