using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface IJsonBuilder
    {
        IInFrameworkBuilder Builder { get; }

        IInFrameworkBuilder Build();
    }
}
