using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface ILoggerBuilder
    {
        IInFrameworkBuilder Builder { get; }
        IInFrameworkBuilder AddNullLogger();
    }
}
