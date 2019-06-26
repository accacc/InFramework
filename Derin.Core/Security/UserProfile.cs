using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Security
{
   public interface IUserIdentity
    {
        int UserId { get; set; }
        string UserName { get; set; }
    }

    public interface IUserProfile: IUserIdentity
    {
        bool IsAdmin { get; set; }

        int LanguageId { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string DelegateUserName { get; set; }


        short OrganizationId { get; set; }

        int CountryId { get; set; }

        string UserFullName { get; set; }



        int PositionId { get; set; }

        string Email { get; set; }

        string[] Roles { get; set; }

        string ActiveRole { get; set; }
    }
    public class UserProfile : IUserProfile
    {

        public bool IsAdmin { get; set; }
        public int UserId { get; set; }

        public int LanguageId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DelegateUserName { get; set; }

        public IFormatProvider CurrentCultureProvider { get; set; }

        public short OrganizationId { get; set; }

        public int CountryId { get; set; }

        public string UserName { get; set; }

        //public string UserFullName => configService.GetAppSettingsValue<string>("SchemaPrefix");

        public int PositionId { get; set; }

        public string Email { get; set; }

        public string[] Roles { get; set; }

        public string ActiveRole { get; set; }
        public string UserFullName { get; set; }

        public UserProfile()
        {

        }



    }
}
