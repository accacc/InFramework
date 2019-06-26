using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Security
{
    

    public interface IInFrameworkAuthorization
    {
        bool IsOnlyAuthenticated { get; set; }
        string DependedActionName { get; set; }
        bool DenyDummyUser { get; set; }
        bool AllowAnonymous { get; set; }
        string PermissionCode { get; set; }
    }
}
