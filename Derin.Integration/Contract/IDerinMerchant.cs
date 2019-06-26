using Derin.Integration.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Integration.Contract
{
    public interface IDerinMerchant
    {

        string SubMerchantExternalId { get; set; }

        string Address { get; set; }

        string ContactName { get; set; }

        string ContactSurname { get; set; }

        string Email { get; set; }


        string GsmNumber { get; set; }

        string Name { get; set; }


        string IBAN { get; set; }

        string IdentityNumber { get; set; }

        string Currency { get; set; }

        string SubMerchantKey { get; set; }

        DerinSubMerchantType SubMerchantType { get; set; }
        string TaxNumber { get; set; }
        string TaxOffice { get; set; }
        string LegalCompanyTitle { get; set; }


    }
}
