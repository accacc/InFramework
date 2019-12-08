using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{

    public interface IModuleBuilder
    {
        IInFrameworkBuilder Builder { get; }
        IInFrameworkBuilder Build();
    }
}
