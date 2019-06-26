using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Integration.Contract
{
    public interface IDerinBuyer
    {
         String Id { get; set; }
         String Name { get; set; }
         String Surname { get; set; }
         String IdentityNumber { get; set; }
         String Email { get; set; }
         String GsmNumber { get; set; }
         String RegistrationDate { get; set; }
         String LastLoginDate { get; set; }
         String RegistrationAddress { get; set; }
         String City { get; set; }
         String Country { get; set; }
         String ZipCode { get; set; }
         String Ip { get; set; }
    }
}
