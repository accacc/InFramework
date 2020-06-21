using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace IF.Rest.Client
{
    public static class CookieExtentions
    {

        public static string GetClientIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        public static void SetCookiePolicyAccept(this HttpContext context)
        {
            context.Response.Cookies.Append("__pa", true.ToString(), new CookieOptions
            {
                Expires = new DateTimeOffset(2100, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))
            });
        }
        public static bool GetCookiePolicyAccept(this HttpContext context)
        {
            var pa = context.Request.Cookies["__pa"];
            return String.IsNullOrEmpty(pa) ? false : bool.Parse(pa);
        }
    }

    public static class UserExtended
    {
        public static string GetFromClaim(this IPrincipal user, string key)
        {
            var myClaim = typeof(IFClaimTypes).GetFields().FirstOrDefault(x => x.Name.ToLower().Contains(key));
            if (myClaim == null) return null;
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(myClaim.GetValue(null).ToString());
            if (claim == null) return null;

            return claim.Value;
        }
        public static int GetId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(IFClaimTypes.UserId);
            return claim == null ? 0 : int.Parse(claim.Value);
        }


    }

    public class IFClaimTypes
    {
        public const string Token = "/identity/claims/token";
        public const string UserId = "/identity/claims/userid";
        public const string UserName = "/identity/claims/username";
        public const string UserFullName = "/identity/claims/name";
        public const string Email = "/identity/claims/email";
        public const string MobilePhone = "/identity/claims/mobilephone";
        public const string Role = "/identity/claims/role";        
    }
}
