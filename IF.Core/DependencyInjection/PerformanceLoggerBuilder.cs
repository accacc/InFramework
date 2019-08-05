using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class PerformanceLoggerBuilder: IPerformanceLoggerBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public PerformanceLoggerBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }
    }
}
