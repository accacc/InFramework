using IF.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Model
{
    public interface  IIdentityBuilder
    {
        IInFrameworkBuilder Builder { get; }

        IInFrameworkBuilder Build<T>() where T : IIdentityService;

    }
}
