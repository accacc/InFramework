using IF.CodeGeneration.Core;

using System;
using System.Collections.Generic;

namespace IF.CodeGeneration.Language.CSharp
{
    public class CSProperty : IGenerateCode
    {

        public string GenericType { get; set; }
        public Type PropertyType { get; set; }
        public string AccessType { get; set; }
        public string Name { get; set; }

        public bool IsNullable { get; set; }

        public bool IsReadOnly { get; set; }

        public string PropertyTypeString { get; set; }

        public List<string> Attirubites { get; set; }

        public CSProperty(Type type, string accessType, string Name, bool IsNullable)
        {
            this.PropertyType = type;
            this.AccessType = accessType;
            this.Name = Name;
            this.IsNullable = IsNullable;
            this.Attirubites = new List<string>();
        }

        public CSProperty(string accessType, string Name, bool IsNullable)
        {            
            this.AccessType = accessType;
            this.Name = Name;
            this.IsNullable = IsNullable;
            this.Attirubites = new List<string>();
        }

        public CodeTemplate GenerateCode()
        {
            //string codeTemplate = @"{0} {1} {2} {{ get; set; }}";            

            string type = this.PropertyTypeString;

            if(type==null)
            {
                type = PropertyType.ToString();
            }

            if (IsNullable && PropertyType != typeof(System.String))
            {
                type = type + "?";
            }
            if(GenericType!=null)
            {
                type = $"{GenericType}<{type}>";
            }

            string @readonly = "";
            string getset = "{ get; set; }";

            if (this.IsReadOnly)
            {
                @readonly = "readonly";
                getset = ";";
            }

            CodeTemplate template = new CodeTemplate();

            foreach (var att in this.Attirubites)
            {
                template.Template += $"[{att}]" + Environment.NewLine;
                
            }

            template.Template += $"{AccessType} {@readonly} {type} {Name} {getset}";

            return template;
        }

       
    }
}
