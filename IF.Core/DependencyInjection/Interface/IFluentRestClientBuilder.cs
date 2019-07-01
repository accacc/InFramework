using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface IRestClientBuilder
    {

        IInFrameworkBuilder Builder { get; }

        IInFrameworkBuilder Build();
    }
}
