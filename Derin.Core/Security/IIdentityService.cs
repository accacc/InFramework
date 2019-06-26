using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Derin.Core.Security
{
    public interface IIdentityService
    {
        void SetUserProfile(IUserIdentity userIdentity);
    }

    public class IdentityService : IIdentityService
    {
        public void  SetUserProfile(IUserIdentity userIdentity)
        {
            if (Thread.CurrentPrincipal == null) return;

            ClaimsPrincipal claims = Thread.CurrentPrincipal as ClaimsPrincipal;

            var userIdClaim = claims.Claims.Where(c => c.Type == ClaimTypes.Sid).SingleOrDefault();

            if (userIdClaim != null)
            {
                userIdentity.UserId = System.Convert.ToInt32(userIdClaim.Value);
            }


            var userNameClaim = claims.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault();

            if (userNameClaim != null)
            {
                userIdentity.UserName = userNameClaim.Value;
            }

            
        }
    }

   
}
