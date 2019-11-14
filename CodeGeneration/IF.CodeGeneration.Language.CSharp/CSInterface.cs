using IF.CodeGeneration.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.CSharp
{

    public class CSInterface : IGenerateCode
    {

        public CSInterface()
        {
            this.Properties = new List<CSProperty>();
            this.InheritedInterfaces = new List<string>();
        }

        public List<CSProperty> Properties { get; set; }
        public string Name { get; set; }

        public List<string> InheritedInterfaces { get; set; }



        public CodeTemplate GenerateCode()
        {

            StringBuilder builder = new StringBuilder("public interface " + Name);
           

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

            CodeTemplate template = new CodeTemplate();
            template.Template = builder.ToString();
            template.CodeTemplateName = this.Name;
            return template;


        }


    }
}

