using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application
{
    public abstract class ApplicationCodeGenerateItem
    {

        public ApplicationCodeGeneratorContext Context { get; set; }
        public ApplicationCodeGenerateItem(ApplicationCodeGeneratorContext context)
        {
            this.Context = context;
        }

        public VSFileType FileType { get; set; }
        public abstract void Execute();

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

        public string GetDataInsertCommandIntarfaceName()
        {
            return $"I{this.Context.className}DataCommandAsync";
        }

        public string GetDataInsertCommandClassName()
        {
            return $"{this.Context.className}DataCommandAsync";
        }

        public string GetDataUpdateCommandIntarfaceName()
        {
            return $"I{this.Context.className}DataCommandAsync";
        }

        //public string GetDataUpdateCommandClassName()
        //{
        //    return $"{this.Context.className}DataCommandAsync";
        //}

        public IFVsFile GetVsFile()
        {
            return this.Context.Files.SingleOrDefault(f => f.FileType == this.FileType);
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
    }
}
