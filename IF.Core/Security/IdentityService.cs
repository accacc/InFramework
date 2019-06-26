using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Security
{
    public class IdentityService : IIdentityService
    {

        private readonly ClaimsPrincipal principal;

        public IdentityService(IPrincipal principal)
        {
            this.principal = principal as ClaimsPrincipal;
        }


        public void SetUserProfile(IUserIdentity userIdentity)
        {
            if (this.principal == null) return;


            var userIdClaim = principal.Claims.Where(c => c.Type == "UserId").SingleOrDefault();

            if (userIdClaim != null)
            {
                userIdentity.UserId = System.Convert.ToInt32(userIdClaim.Value);
            }


            var userNameClaim = principal.Claims.Where(c => c.Type == "UserName").SingleOrDefault();

            if (userNameClaim != null)
            {
                userIdentity.UserName = userNameClaim.Value;
            }

            var channelId = principal.Claims.Where(c => c.Type == "Channel").SingleOrDefault();

            if (channelId != null)
            {
                userIdentity.ApplicationCode = channelId.Value;
            }

            var clientIp = principal.Claims.Where(c => c.Type == "IpAddress").SingleOrDefault();

            if (clientIp != null)
            {
                userIdentity.ClientIp = clientIp.Value;
            }


        }
    }
}
