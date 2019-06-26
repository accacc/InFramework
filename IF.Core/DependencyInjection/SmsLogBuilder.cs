using IF.Core.DependencyInjection.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    

    public class SmsLoggerBuilder : ISmsLoggerBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public SmsLoggerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }
    }
}
