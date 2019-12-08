using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IF.Core.DependencyInjection.Interface;
using IF.Core.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IF.Module.Dictionary
{
    public static class ModuleIntegrationExtension
    {
        public static IModuleBuilder  (this IModuleBuilder moduleBuilder, IMvcBuilder mvc)
        {           


            var assemblies = new string[] { "IF.Module.Dictionary" };

            mvc.ConfigureApplicationPartManager(apm =>
            {

                foreach (var assemblyFile in assemblies)
                {

                      //main assembly
                      var assembly = Assembly.Load(assemblyFile);
                    apm.ApplicationParts.Add(new AssemblyPart(assembly));

                      //view assembly
                      var assemblyView = Assembly.Load(assemblyFile + ".Views");
                    apm.ApplicationParts.Add(new CompiledRazorAssemblyPart(assemblyView));
                }
            });



            return moduleBuilder;
        }
    }
}
