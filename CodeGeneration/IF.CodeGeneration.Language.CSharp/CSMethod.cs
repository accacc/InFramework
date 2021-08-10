using IF.CodeGeneration.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Language.CSharp
{
    public class CSMethod : IGenerateCode
    {

        public CSMethod(string Name, string ReturnType, string AccessType)
        {
            this.Name = Name;
            this.ReturnType = ReturnType;
            this.AccessType = AccessType;
            this.Parameters = new List<CsMethodParameter>();
            this.Attirubites = new List<string>();

        }

        public List<CsMethodParameter> Parameters { get; set; }

        public List<string> Attirubites { get; set; }
        public string AccessType { get; set; }
        public string ReturnType { get; set; }

        public bool IsAsync { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public bool IsConstructor { get; set; }

        public bool IsStatic { get; set; }

        public bool IsOvveride { get; set; }

        public CodeTemplate GenerateCode()
        {

            string @params = String.Empty;
            string @baseParams = String.Empty;


            foreach (var parameter in this.Parameters)
            {
                if (!String.IsNullOrWhiteSpace(parameter.Attirubite))
                {
                    @params += $"[{parameter.Attirubite}] ";
                }



                @params += String.Format("{0} {1}", parameter.Type, parameter.Name);

                if (this.Parameters.Last().Name != parameter.Name)
                {
                    @params += ",";
                }
            }
            if (this.IsConstructor)
            {
                ReturnType = String.Empty;

                foreach (var parameter in this.Parameters.Where(p => p.UseBase))
                {



                    @baseParams += parameter.Name;

                    if (this.Parameters.Last().Name != parameter.Name)
                    {
                        @baseParams += ",";
                    }
                }

                baseParams = $" : base ({baseParams})";
            }


            string accessType = this.AccessType;
            string returnType = this.ReturnType;
            string @static = String.Empty;
            string name = this.Name;
            string @override = String.Empty;

            if (this.IsOvveride)
            {
                @override = "override";
            }

            if (this.IsStatic)
            {
                @static = "static";
            }

            if (this.IsAsync)
            {
                accessType = this.AccessType + " async";

                if (this.ReturnType != "void" && this.ReturnType != null)
                {

                    returnType = $"Task<{this.ReturnType}>";
                }
                else
                {
                    returnType = "Task";
                }

            }

            StringBuilder builder = new StringBuilder();

            foreach (var att in this.Attirubites)
            {
                builder.AppendLine($"[{att}]");
            }

            builder.AppendLine($"{accessType} {@static} {@override} {returnType} {name} ({@params}) {baseParams}");

            builder.AppendLine("{");

            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine(this.Body);
            builder.AppendLine("}");

            CodeTemplate template = new CodeTemplate();
            template.Template = builder.ToString();
            template.CodeTemplateName = this.Name;
            return template;

        }
    }


    public class CsMethodParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public bool UseBase { get; set; }
        public string Attirubite { get; set; }
    }
}
