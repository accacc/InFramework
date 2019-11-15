using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;

using System;

namespace IF.CodeGeneration.Application.Generator
{
    public abstract class CSGeneratorBase
    {

        public readonly FileSystemCodeFormatProvider fileSystem;
        protected string BaseCommandName = "BaseCommand";
        protected string className;
        protected string nameSpaceName;
        protected ClassTree classTree;
        protected Type classType;



        public CSGeneratorBase(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType)
        {
            this.fileSystem = fileSystem;
            this.className = className;
            this.nameSpaceName = nameSpaceName;
            this.classTree = classTree;
            this.classType = classType;
        }


        public CSClass GenerateClass(string plus = "")
        {
            CSClass @class = new CSClass();

            @class.Name = className + plus;

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                @class.Properties.Add(classProperty);
            }

            return @class;
        }

        public CSProperty GetClassProperty(string propertyName)
        {
            var property = classType.GetProperty(propertyName);
            var classProperty = new CSProperty(property.PropertyType, "public", property.Name, property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
            return classProperty;
        }

        public string GetDataQueryIntarfaceName()
        {
            return $"I{className}DataQueryAsync";
        }

        public string GetDataQueryClassName()
        {
            return $"{className}DataQueryAsync";
        }
    }
}
