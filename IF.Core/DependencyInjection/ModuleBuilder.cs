using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class ModuleBuilder : IModuleBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public ModuleBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        public IInFrameworkBuilder Build()
        {
            return this.Builder;
        }
    }
}
