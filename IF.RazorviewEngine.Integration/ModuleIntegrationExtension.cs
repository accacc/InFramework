using IF.Core.DependencyInjection;
using IF.Core.DependencyInjection.Model;
using IF.Core.Module;
using IF.RazorViewEngine;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;

namespace IF.RazorviewEngine.Integration
{
    public static class ModuleIntegrationExtension
    {
        public static IRazorTemlateBuilder AddModule(this IRazorTemlateBuilder razorTemplateBuilder,IServiceCollection services, params string[] moduleNames)
        {            

            ModuleManager moduleManager  = ModuleManager.Current;
            
            moduleManager.RegisterAllModules(moduleNames);

            services.AddSingleton<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            

            services.AddMvc().AddRazorOptions(o =>
            {
                if (moduleManager.Modules != null)
                {
                    foreach (var module in moduleManager.Modules)
                    {
                        System.Reflection.Assembly assembly = module.Value;
                        string resourceNamespace = assembly.GetName().Name;
                        EmbeddedFileProvider fileProvider = new EmbeddedFileProvider(assembly, module.Key.Name);
                        o.FileProviders.Add(fileProvider);
                    }
                }
            }).ConfigureApplicationPartManager(apm =>
            {
                if (moduleManager.Modules != null)
                {
                    foreach (Assembly assembly in moduleManager.Modules.Values)
                    {
                        var part = new AssemblyPart(assembly);
                        apm.ApplicationParts.Add(part);
                    }
                }
            });

            return razorTemplateBuilder;
        }
    }
}
