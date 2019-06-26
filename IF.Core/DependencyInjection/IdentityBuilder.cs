using IF.Core.DependencyInjection.Model;
using IF.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class IdentityBuilder : IIdentityBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public IdentityBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;

        }

        public IInFrameworkBuilder Build<T>() where T : IIdentityService
        {
            this.Builder.RegisterType<T, IIdentityService>(DependencyScope.PerInstance);
            return this.Builder;
        }
    }
}
