using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface IEventBusBuilder
    {
        IInFrameworkBuilder Builder { get; }
        IInFrameworkBuilder Build(Assembly[] assemblies);
    }
}
