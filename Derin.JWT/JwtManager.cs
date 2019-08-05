using IF.Core.Reflection;
using IF.Core.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Derin.JWT
{
    public static class JwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string GenerateToken<T>(Action<ClaimBuilder<T>> claimBuilder, int expireMinutes = 20) where T : IUserProfile, new()

        {
            ClaimBuilder<T> profile = new ClaimBuilder<T>();

            claimBuilder(profile);

            return GenerateToken(profile.Claims);
        }

        public static string GenerateToken<T>(T profile,int expireMinutes = 20, params Expression<Func<T, object>>[] expressions) where T : IUserProfile
        {
            ClaimsIdentity claims = new ClaimsIdentity();

            foreach (var expression in expressions)
            {
                var propertyInfo = ReflectionHelper.GetPropertyInfo(profile, expression);


                claims.AddClaim(new Claim(propertyInfo.Name, propertyInfo.GetValue(profile, null).ToString()));
            }

            return GenerateToken(claims);
        }

     



     


        private static string GenerateToken(ClaimsIdentity claim, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claim,

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static T GetPrincipal<T>(string token) where T : IUserProfile, new()

        {
            T profile = new T();

            ClaimsPrincipal claimsPrincipal = GetPrincipal(token);

            foreach (var claim in claimsPrincipal.Claims)
            {
                //TODO:Caglar hepsini set edicez mi bazilarini .net kendi set ediyor
                ReflectionHelper.SetPropertyValueFromString(profile, claim.Type, claim.Value);
            }

            return profile;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;

                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}