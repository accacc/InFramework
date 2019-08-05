using IF.Core.DependencyInjection.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public class EmailBuilder:IEmailSenderBuilder
    {
        public IInFrameworkBuilder Builder { get; }

        public EmailBuilder(IInFrameworkBuilder dependencyInjection)
        {
            this.Builder = dependencyInjection;
        }

        public IInFrameworkBuilder Build(Assembly[] assemblies)
        {
            return this.Builder;
        }
    }
}
