using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;

using System;

namespace IF.CodeGeneration.Application.Generator
{
    public abstract class CSGeneratorBase
    {

        public readonly FileSystemCodeFormatProvider fileSystem;
        protected string BaseCommandName = "BaseCommand";


        public CSGeneratorBase(FileSystemCodeFormatProvider fileSystem)
        {
            this.fileSystem = fileSystem;
        }


        public CSClass GenerateClass(string className, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();

            @class.Name = className;

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(classType, property.Name.Split('\\')[2]);
                @class.Properties.Add(classProperty);
            }

            return @class;
        }

        public CSProperty GetClassProperty(Type classType, string propertyName)
        {
            var property = classType.GetProperty(propertyName);
            var classProperty = new CSProperty(property.PropertyType, "public", property.Name, property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
            return classProperty;
        }
    }
}
