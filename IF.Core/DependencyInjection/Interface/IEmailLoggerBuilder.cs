using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface IEmailLoggerBuilder
    {
        IInFrameworkBuilder Builder { get; }
    }
}
