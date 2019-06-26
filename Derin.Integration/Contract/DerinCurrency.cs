using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Integration.Contract
{
    public sealed class DerinCurrency
    {
        private readonly String value;

        public static readonly DerinCurrency TRY = new DerinCurrency("TRY");
        public static readonly DerinCurrency EUR = new DerinCurrency("EUR");
        public static readonly DerinCurrency USD = new DerinCurrency("USD");
        public static readonly DerinCurrency GBP = new DerinCurrency("GBP");
        public static readonly DerinCurrency IRR = new DerinCurrency("IRR");
        public static readonly DerinCurrency NOK = new DerinCurrency("NOK");
        public static readonly DerinCurrency RUB = new DerinCurrency("RUB");
        public static readonly DerinCurrency CHF = new DerinCurrency("CHF");

        private DerinCurrency(String value)
        {
            this.value = value;
        }

        public override String ToString()
        {
            return value;
        }
    }
}
