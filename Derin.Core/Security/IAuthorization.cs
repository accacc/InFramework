using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Security
{
    public interface IAuthorizationService
    {
        bool OnAuthorization();
    }
}
