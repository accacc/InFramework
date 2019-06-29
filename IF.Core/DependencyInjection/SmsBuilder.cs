using IF.Core.DependencyInjection.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class SmsBuilder:ISmsBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public SmsBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        public IInFrameworkBuilder Build(Assembly[] assemblies)
        {
            return this.Builder;
        }
    }
}
