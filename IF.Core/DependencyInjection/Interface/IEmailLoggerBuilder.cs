using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Model
{
    public interface IEmailLoggerBuilder
    {
        IInFrameworkBuilder Builder { get; }
    }
}
