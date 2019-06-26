using Derin.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Enum
{
    [DataContract(Name = "Currency")]
    public enum CurrencyType
    {
        [EnumMember]
        [ObjectDescription("TRY")]
        TRY,

        [EnumMember]
        [ObjectDescription("GBP")]
        GBP,

        [EnumMember]
        [ObjectDescription("EUR")]
        EUR,

        [EnumMember]
        [ObjectDescription("USD")]
        USD,

        [EnumMember]
        [ObjectDescription("UAH")]
        UAH,

        [EnumMember]
        [ObjectDescription("AZN")]
        AZN

    }
}
