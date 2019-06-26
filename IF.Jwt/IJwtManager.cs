using IF.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace IF.Jwt
{
    public interface IJwtManager
    {
        string GenerateToken<T>(Action<ClaimBuilder<T>> claimBuilder) where T : IUserProfile, new();
        string GenerateToken<T>(T profile,params Expression<Func<T, object>>[] expressions) where T : IUserProfile;

        ClaimsPrincipal GetPrincipal(string token);
        T GetPrincipal<T>(string token) where T : IUserProfile, new();

        string GetSchemeName();
    }
}
