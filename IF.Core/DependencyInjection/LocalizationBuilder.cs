using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class LocalizationBuilder : ILocalizationBuilder
    {
        public IInFrameworkBuilder Container { get; }

        public LocalizationBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Container = dependencyInjection;
        }
    }
}
