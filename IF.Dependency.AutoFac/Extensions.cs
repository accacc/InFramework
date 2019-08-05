using Autofac;
using Autofac.Extensions.DependencyInjection;
using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Dependency.AutoFac
{
    public static class Extensions
    {
        public static IServiceProvider Build(this IServiceCollection services, IInFrameworkBuilder di)
        {
            InFrameworkBuilder autofac = di as InFrameworkBuilder;

            autofac.builder.Populate(services);
            //builder.RegisterModule<FrameworkModule>();
            var container = autofac.builder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
