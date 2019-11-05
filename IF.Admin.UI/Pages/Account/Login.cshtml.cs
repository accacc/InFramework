using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IF.Admin.UI.Pages.Account
{

    [AllowAnonymous]
    public class LoginModel : PageModel
    {


        public async Task<IActionResult> OnPost(string userName, string password)
        {


            if (userName == "if" && password == "111111")
            {
                var claims = new List<Claim>{
                      new Claim(ClaimTypes.Name, userName)

                 };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),

                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return new EmptyResult();


            }

            return Redirect("/Account/Login");

        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
        }


        private IEnumerable<Claim> GetUserClaims(string username)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, username));

            return claims;
        }
    }
}
