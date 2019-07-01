using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class RestClientBuilder: IRestClientBuilder
    {

        public IInFrameworkBuilder Builder { get; }

        public RestClientBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        public IInFrameworkBuilder Build()
        {
            return this.Builder;
        }
    }
}
