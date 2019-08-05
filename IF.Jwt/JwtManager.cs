using IF.Core.Exception;
using IF.Core.Jwt;
using IF.Core.Reflection;
using IF.Core.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;

namespace IF.Jwt
{
    public class JwtManager : IJwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>

        private readonly JwtSettings jwtSettings;

        public JwtManager(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }




        public string GenerateToken<T>(Action<ClaimBuilder<T>> claimBuilder) where T : IUserProfile, new()

        {
            ClaimBuilder<T> profile = new ClaimBuilder<T>();

            claimBuilder(profile);

            return GenerateToken(profile.Claims, jwtSettings.ExpireMinutes);
        }

        public string GenerateToken<T>(T profile, params Expression<Func<T, object>>[] expressions) where T : IUserProfile
        {
            ClaimsIdentity claims = new ClaimsIdentity();

            foreach (var expression in expressions)
            {
                var propertyInfo = ReflectionHelper.GetPropertyInfo(profile, expression);

                claims.AddClaim(new Claim(propertyInfo.Name, propertyInfo.GetValue(profile, null).ToString()));
            }

            return GenerateToken(claims, jwtSettings.ExpireMinutes);
        }

        private string GenerateToken(ClaimsIdentity claim, int expireMinutes = 20)
        {
            if (String.IsNullOrWhiteSpace(jwtSettings.SecretKey))
            {
                throw new IFApplicationException("Jwt Secret Key Not Found");
            }

            var symmetricKey = Convert.FromBase64String(jwtSettings.SecretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.Now;

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

        public T GetPrincipal<T>(string token) where T : IUserProfile, new()

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

        public ClaimsPrincipal GetPrincipal(string token)
        {

            if (String.IsNullOrWhiteSpace(jwtSettings.SecretKey))
            {
                throw new IFApplicationException("Jwt Secret Key Not Found");
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(jwtSettings.SecretKey);

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

            catch
            {
                return null;
            }
        }

        public string GetSchemeName()
        {
            return this.jwtSettings.SchemeName;
        }
    }
}