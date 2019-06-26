using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface IJsonBuilder
    {
        IInFrameworkBuilder Builder { get; }

        IInFrameworkBuilder Build();
    }
}
