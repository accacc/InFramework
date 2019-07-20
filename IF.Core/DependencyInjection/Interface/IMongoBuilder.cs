using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public interface IMongoBuilder
    {
        IInFrameworkBuilder Container { get; }

        
    }
}
