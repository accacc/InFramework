using FOFramework.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOFramework.CodeGeneration.CSharp
{
    public class CSProperty : IGenerateCode
    {

        public Type PropertyType { get; set; }
        public string AccessType { get; set; }
        public string Name { get; set; }

        public bool IsNullable { get; set; }

        public string PropertyTypeString { get; set; }

        public CSProperty(Type type, string accessType, string Name, bool IsNullable)
        {
            this.PropertyType = type;
            this.AccessType = accessType;
            this.Name = Name;
            this.IsNullable = IsNullable;
        }

        public CodeTemplate GenerateCode()
        {
            string codeTemplate = @"{0} {1} {2} {{ get; set; }}";

            CodeTemplate template = new CodeTemplate();

            string type = this.PropertyTypeString;

            if(type==null)
            {
                type = PropertyType.ToString();
            }

            if (IsNullable && PropertyType != typeof(System.String))
            {
                type = type + "?";
            }

            template.Template = String.Format(codeTemplate, AccessType, type,Name);

            return template;
        }

       
    }
}
