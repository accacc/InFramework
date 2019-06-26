using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Integration.Contract
{
    public interface IDerinBasketItem
    {
        
         String Id { get; set; }
         String Price { get; set; }
         String Name { get; set; }
         String Category1 { get; set; }
         String Category2 { get; set; }
         String ItemType { get; set; }
         String SubMerchantKey { get; set; }
         String SubMerchantPrice { get; set; }

        //IDerinMerchant Saler { get; set; }
    }
}
