using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Security
{
    public interface ITokenService
    {
        string GenerateToken(string username, int expireMinutes = 20);
        ClaimsPrincipal GetPrincipal(string token);
    }
}
