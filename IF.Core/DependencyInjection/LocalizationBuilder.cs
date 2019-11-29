using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class LocalizationBuilder : ILocalizationBuilder
    {
        public IInFrameworkBuilder Builder { get; }


        public LocalizationBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        
        }

        public IInFrameworkBuilder Build()
        {            
            return this.Builder;
        }
    }
}
