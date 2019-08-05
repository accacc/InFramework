using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface IApplicationLoggerBuilder
    {
        IInFrameworkBuilder Builder { get; }
        IInFrameworkBuilder AddNullLogger();
    }
}
