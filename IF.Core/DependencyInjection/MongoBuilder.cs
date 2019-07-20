using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class MongoBuilder:IMongoBuilder
    {
        public IInFrameworkBuilder Container { get; }

        public MongoBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Container = dependencyInjection;
        }

       
    }
}
