using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class AuditLoggerBuilder : IAuditLoggerBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public AuditLoggerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }
    }
}
