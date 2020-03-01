using IF.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.CSharp
{
    public class CSClass : IGenerateCode
    {

        public CSClass()
        {
            this.Properties = new List<CSProperty>();
            this.Methods = new List<CSMethod>();
            this.InheritedInterfaces = new List<string>();
            this.Usings = new List<string>();
        }

        public List<CSProperty> Properties { get; set; }

        public List<CSMethod> Methods { get; set; }
        public string Name { get; set; }

        public string NameSpace { get; set; }

        public List<string> InheritedInterfaces { get; set; }

        public List<string> Usings { get; set; }

        public string BaseClass { get; set; }


        public CodeTemplate GenerateCode()
        {

            StringBuilder builder = new StringBuilder();


            foreach (var @using in this.Usings)
            {
                builder.AppendLine($"using {@using};");
            }

            builder.AppendLine();

            if(!String.IsNullOrWhiteSpace(this.NameSpace))
            {
                
                builder.AppendLine($"namespace {this.NameSpace}");
                builder.AppendLine("{");
            }

            builder.Append("public class " + Name);

            if (!String.IsNullOrEmpty(this.BaseClass))
            {
                builder.Append(":" + BaseClass);
            }
            //else
            //{
            //    builder.AppendLine();
            //}

            if (this.InheritedInterfaces != null && InheritedInterfaces.Any())
            {
                if (String.IsNullOrEmpty(this.BaseClass))
                {
                    builder.Append(" : ");
                }
                else
                {
                    builder.Append(" , ");
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

            if (!String.IsNullOrWhiteSpace(this.NameSpace))
            {
                builder.AppendLine("}");
            }

            CodeTemplate template = new CodeTemplate();
            template.Template = builder.ToString();
            template.CodeTemplateName = this.Name;
            return template;


        }

        
    }
}
