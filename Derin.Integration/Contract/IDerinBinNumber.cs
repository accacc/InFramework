using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Integration.Contract
{
     public interface IDerinBinNumber
    {
         String Bin { get; set; }
         String CardType { get; set; }
         String CardAssociation { get; set; }
         String CardFamily { get; set; }
         String BankName { get; set; }
         long BankCode { get; set; }
    }
}
