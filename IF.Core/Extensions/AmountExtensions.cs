using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IF.Core.Extensions
{
    public static class AmountExtentions
    {

        public static decimal ToTurkishAmount(this string amount)
        {
            amount = amount.Replace(".", ",");
            return decimal.Parse(amount, CultureInfo.GetCultureInfo("tr-TR"));
        }

        public static string ToEnglishStringAmount(this decimal amount)
        {
            return amount.ToString().Replace(",", ".");
        }

        public static string ToEnglishStringAmount(this string amount)
        {
            return amount.Replace(",", ".");
        }
    }
}
