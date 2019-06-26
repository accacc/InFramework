using IF.Core.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace IF.Jwt
{
    public static class AuthenticationBuilderExtensions
    {

        public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder builder,  JwtSettings settings , Action<JwtAuthOptions> configureOptions =null)
        {


            builder.Services.AddSingleton<IJwtManager>(new JwtManager(settings));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            return builder.AddScheme<JwtAuthOptions, JwtAuthenticationHandler>(settings.SchemeName,configureOptions);
        }
    }
}
