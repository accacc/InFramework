using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IF.Core.Data;

namespace IF.CodeGeneration.Application
{
    public class ReflectClass<T>
    {
        List<Type> types;
        Assembly ass;

        public ReflectClass(string assembly)
        {

            ass = Assembly.ReflectionOnlyLoadFrom(assembly);
            this.types = FindAllDerivedTypes(ass);
        }

        public Assembly GetAssembly()
        {
            return ass;
        }

        public List<Type> GetTypes()
        {
            return types;
        }


        private static List<Type> FindAllDerivedTypes(Assembly assembly)
        {
            var derivedType = typeof(Entity);

            List<Type> types = new List<Type>();

            foreach (var item in assembly.GetTypes())
            {
                if (item.BaseType != null && derivedType.FullName == item.BaseType.FullName)
                {
                    types.Add(item);
                }
            }

            return types;

        }
    }
}
