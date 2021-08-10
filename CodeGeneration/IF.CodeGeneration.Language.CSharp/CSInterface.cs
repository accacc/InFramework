using IF.CodeGeneration.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Language.CSharp
{

    public class CSInterface : IGenerateCode
    {

        public CSInterface()
        {
            this.Properties = new List<CSProperty>();
            this.InheritedInterfaces = new List<string>();
            this.Usings = new List<string>();
        }


        public string NameSpace { get; set; }
        public List<CSProperty> Properties { get; set; }
        public string Name { get; set; }

        public List<string> InheritedInterfaces { get; set; }

        public List<string> Usings { get; set; }

        public CodeTemplate GenerateCode()
        {

            StringBuilder builder = new StringBuilder();

            foreach (var @using in this.Usings)
            {
                builder.AppendLine($"using {@using};");
            }

            if (!String.IsNullOrWhiteSpace(this.NameSpace))
            {

                builder.AppendLine($"namespace {this.NameSpace}");
                builder.AppendLine("{");
            }

            builder.AppendLine("public interface " + Name);

            if (this.InheritedInterfaces != null && InheritedInterfaces.Any())
            {
                builder.Append(" : ");
                builder.Append(String.Join(",", InheritedInterfaces));
            }

            builder.AppendLine("{");

            builder.AppendLine();
            builder.AppendLine();


            foreach (var property in Properties)
            {
                builder.AppendLine(property.GenerateCode().Template);
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

