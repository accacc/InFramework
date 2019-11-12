using IF.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Tools.CodeGenerator
{

    public class Assembiler
    {
        Dictionary<Assembly, List<Type>> types = new Dictionary<Assembly, List<Type>>();

       public void AddAssemly<T>(string asss) where T :class
        {

            ReflectClass<T> reflectClass = new ReflectClass<T>(asss);

            Assembly ass = reflectClass.GetAssembly();

            if (types.ContainsKey(ass)) types.Remove(ass);

            types.Add(ass, reflectClass.GetTypes());
        }

        public Dictionary<Assembly, List<Type>> AllAssembilies()
        {
            return types;
        }

        public PropertyInfo[] GetProperties(string assName, string typeName)
        {
            foreach (var ass in types.Keys)
            {
                if (ass.GetName().Name == assName)
                {
                    var type = types[ass].Where(t => t.Name == typeName).SingleOrDefault();

                    if (type != null)
                    {
                        return type.GetProperties();
                    }
                }
            }
            return null;
        }

        public Type GetType(string assName, string typeName)
        {
            foreach (var ass in types.Keys)
            {
                if (ass.GetName().Name == assName)
                {
                    var type = types[ass].Where(t => t.Name == typeName).SingleOrDefault();

                    return type;
                }
            }
            return null;
        }

        public string Dump(string assName, string typeName)
        {
            var type = GetType(assName, typeName);
            var obj = Activator.CreateInstance(type);
            string dumped = Dump(obj,"aaaaaa" ,5);
            return dumped;
        }

        public static string Dump(object o, string name = "", int depth = 3)
        {
            try
            {
                var leafprefix = (string.IsNullOrWhiteSpace(name) ? name : name + " = ");

                if (null == o) return leafprefix + "null";

                var t = o.GetType();
                if (depth-- < 1 || t == typeof(string) || t.IsValueType)
                    return leafprefix + o;

                var sb = new StringBuilder();
                var enumerable = o as IEnumerable;
                if (enumerable != null)
                {
                    name = (name ?? "").TrimEnd('[', ']') + '[';
                    var elements = enumerable.Cast<object>().Select(e => Dump(e, "", depth)).ToList();
                    var arrayInOneLine = elements.Count + "] = {" + string.Join(",", elements) + '}';
                    if (!arrayInOneLine.Contains(Environment.NewLine)) // Single line?
                        return name + arrayInOneLine;
                    var i = 0;
                    foreach (var element in elements)
                    {
                        var lineheader = name + i++ + ']';
                        sb.Append(lineheader).AppendLine(element.Replace(Environment.NewLine, Environment.NewLine + lineheader));
                    }
                    return sb.ToString();
                }
                foreach (var f in t.GetFields())
                    sb.AppendLine(Dump(f.GetValue(o), name + '.' + f.Name, depth));
                foreach (var p in t.GetProperties())
                    sb.AppendLine(Dump(p.GetValue(o, null), name + '.' + p.Name, depth));
                if (sb.Length == 0) return leafprefix + o;
                return sb.ToString().TrimEnd();
            }
            catch
            {
                return name + "???";
            }
        }



    }
    public class ReflectClass<T>
    {
        List<Type> types;
        Assembly ass;
        
        public ReflectClass(string assembly)
        {
            
            ass = Assembly.ReflectionOnlyLoadFrom(assembly);
            this.types = FindAllDerivedTypes<T>(ass);
        }

        public Assembly GetAssembly()
        {
            return ass;
        }

        public List<Type> GetTypes()
        {
            return types;
        }


        private static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            var derivedType = typeof(Entity);

            List<Type> types = new List<Type>();

            foreach (var item in assembly.GetTypes())
            {
                if(item.BaseType!=null &&  derivedType.FullName == item.BaseType.FullName)
                {
                    types.Add(item);
                }
            }

            return types;

        }
    }
}
