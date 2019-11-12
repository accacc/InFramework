using FOFramework.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.CodeGeneration.CSharp
{
    public class CSClass : IGenerateCode
    {

        public CSClass()
        {
            this.Properties = new List<CSProperty>();
            this.Methods = new List<CSMethod>();
            this.InheritedInterfaces = new List<string>();
        }

        public List<CSProperty> Properties { get; set; }

        public List<CSMethod> Methods { get; set; }
        public string Name { get; set; }

        public List<string> InheritedInterfaces { get; set; }

        public string BaseClass { get; set; }


        public CodeTemplate GenerateCode()
        {
            
            StringBuilder builder = new StringBuilder("public class " + Name);

            if (!String.IsNullOrEmpty(this.BaseClass))
            {
                builder.Append(":" + BaseClass);
            }

            if (this.InheritedInterfaces != null && InheritedInterfaces.Any())
            {
                if (String.IsNullOrEmpty(this.BaseClass))
                {
                    builder.Append(" : ");
                }

                builder.Append(String.Join(",", InheritedInterfaces));
            }

            builder.AppendLine("{");

            builder.AppendLine();
            builder.AppendLine();


            foreach (var property in Properties)
            {
                builder.AppendLine(property.GenerateCode().Template);
            }

            foreach (var method in Methods)
            {
                builder.AppendLine(method.GenerateCode().Template);
            }

            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("}");

            CodeTemplate template = new CodeTemplate();
            template.Template = builder.ToString();
            template.CodeTemplateName = this.Name;
            return template;


        }

        
    }
}
