using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using IF.Tools.CodeGenerator.VsAutomation;
using System;

namespace IF.CodeGeneration.Application.Generator
{
    public abstract class CSGeneratorBase
    {
        
        public readonly GeneratorContext Context;

        public CSGeneratorBase(GeneratorContext context)
        {
            this.Context = context;            
        }

        public CSClass GenerateClass(string plus = "")
        {
            CSClass @class = new CSClass();

            @class.Name = this.Context.className + plus;

            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                @class.Properties.Add(classProperty);
            }

            return @class;
        }

        public CSProperty GetClassProperty(string propertyName)
        {
            var property = this.Context.classType.GetProperty(propertyName);
            var classProperty = new CSProperty(property.PropertyType, "public", property.Name, property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
            return classProperty;
        }

        public string GetDataQueryIntarfaceName()
        {
            return $"I{this.Context.className}DataQueryAsync";
        }

        public string GetDataQueryClassName()
        {
            return $"{this.Context.className}DataQueryAsync";
        }
    }
}
