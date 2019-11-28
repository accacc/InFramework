using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using IF.Tools.CodeGenerator.VsAutomation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IF.CodeGeneration.Application.Generator
{
    public abstract class CSGeneratorBase
    {
        
        public readonly GeneratorContext Context;

        public abstract void UpdateContext();

        public abstract void SetItemActive(VSFileType type);

        public List<IFVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected VSFileType FileType;

        public CSGeneratorBase(GeneratorContext context)
        {
            this.Context = context;
            this.Files = new List<IFVsFile>();
            this.Items = new List<IGenerateItem>();            
        }

        public IFVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }

        public void Generate()
        {
            //this.UpdateContext();

            foreach (var item in Items)
            {
                item.Execute();
            }
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

        public  string GetDataInsertCommandIntarfaceName()
        {
            return $"I{this.Context.className}DataCommandAsync";
        }

        public  string GetDataInsertCommandClassName()
        {
            return $"{this.Context.className}DataCommandAsync";
        }

        public string GetDataUpdateCommandIntarfaceName()
        {
            return $"I{this.Context.className}DataCommandAsync";
        }

        public string GetDataUpdateCommandClassName()
        {
            return $"{this.Context.className}DataCommandAsync";
        }
    }
}
