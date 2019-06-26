using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Derin.Core.Session
{
    public interface ISingleLoginProvider
    {
        void SetUserIsOnline(string userName);
        bool IsAlreadyLogined();
    }
}
