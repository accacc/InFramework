using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class EmailLoggerBuilder : IEmailLoggerBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public EmailLoggerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }
    }
}
