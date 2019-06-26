using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IF.Core.Module
{
    public class ModuleManager
    {



        private static ModuleManager _current;
        public static ModuleManager Current
        {
            get { return _current ?? (_current = new ModuleManager()); }
        }


        public Dictionary<IModule, Assembly> Modules { get; set; }

        public ModuleManager()
        {
            Modules = new Dictionary<IModule, Assembly>();           
        }

        public void RegisterAllModules(params string[] assemblyNames)
        {            

            var assemblies = assemblyNames.Select(x => Assembly.Load(x));

            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().Where(t => t.GetInterface(typeof(IModule).Name) != null).FirstOrDefault();

                if (type != null)
                {
                    var module = (IModule)Activator.CreateInstance(type);
                    _current.Modules.Add(module, assembly);
                }
            }
        }

      

        public IEnumerable<IModule> GetModules()
        {
            return Modules.Select(m => m.Key).ToList();
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return Modules.Select(m => m.Value).ToList();
        }

        public IModule GetModule(string name)
        {
            return GetModules().Where(m => m.Name == name).FirstOrDefault();
        }

        public Assembly GetAssembly(string name)
        {
            return GetAssemblies().Where(m => m.FullName == name).FirstOrDefault();
        }


        public Assembly GetAssemblyByModuleName(string name)
        {
            var module = this.GetModule(name);

            Assembly assembly = this.Modules.Where(m => m.Key.Name == module.Name).SingleOrDefault().Value;

            return assembly;
        }

    }
}
