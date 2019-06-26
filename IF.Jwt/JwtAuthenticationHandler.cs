using IF.Core.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IF.Jwt
{



    public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthOptions>, IInFrameworkAuthorization
    {
        private readonly IJwtManager jwtManager;
        public JwtAuthenticationHandler(IOptionsMonitor<JwtAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IJwtManager jwtManager) : base(options, logger, encoder, clock)
        {
            this.jwtManager = jwtManager;
        }

        public bool IsOnlyAuthenticated { get; set; }
        public string DependedActionName { get; set; }
        public bool DenyDummyUser { get; set; }
        public bool AllowAnonymous { get; set; }
        public string PermissionCode { get; set; }

        public string Realm { get; set; }
        public bool AllowMultiple => false;

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if(StringValues.IsNullOrEmpty(Request.Headers[HeaderNames.Authorization]))
            {
                return Fail("Missing Authorization Header");
            }

            AuthenticationHeaderValue authorization = AuthenticationHeaderValue.Parse(Request.Headers[HeaderNames.Authorization]);

            if (authorization == null || authorization.Scheme != "Bearer")
            {                
                return Fail("Missing Authorization Header");
            }


            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                return Fail("Missing Jwt Token");
            }

            var token = authorization.Parameter;

            var principal = ValidateToken(token);

            if (principal == null)
            {
                return Fail("Invalid token");
            }
            else
            {
                var ticket = new AuthenticationTicket(principal,jwtManager.GetSchemeName());
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }

       

      

        protected ClaimsPrincipal ValidateToken(string token)
        {
            var principle = this.jwtManager.GetPrincipal(token);

            var identity = principle?.Identity as ClaimsIdentity;

            if (identity == null)
                return null;

            if (!identity.IsAuthenticated)
                return null;

            var usernameClaim = identity.FindFirst("UserName");

            string username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return null;

            return principle;
        }

        private Task<AuthenticateResult> Fail(string message)
        {
            return Task.FromResult(AuthenticateResult.Fail(message));
        }

    }

   

   
}