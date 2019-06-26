using IF.Core.DependencyInjection.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class RazorTemlateBuilder : IRazorTemlateBuilder
    {
        public IInFrameworkBuilder Builder { get; }


        public RazorTemlateBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        
        }

        public IInFrameworkBuilder Build()
        {            
            return this.Builder;
        }
    }
}
