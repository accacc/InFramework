﻿using FOFramework.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.CodeGeneration.CSharp
{
    public class CSMethod : IGenerateCode
    {

        public CSMethod(string Name,string ReturnType,string AccessType)
        {
            this.Name = Name;
            this.ReturnType = ReturnType;
            this.AccessType = AccessType;
            this.Parameters = new List<CsMethodParameter>();
        }

        public List<CsMethodParameter>   Parameters { get; set; }

        public string AccessType { get; set; }
        public string ReturnType { get; set; }

        public bool IsAsync { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }
        public CodeTemplate GenerateCode()
        {

            string @params = String.Empty;

            foreach (var parameter in this.Parameters)
            {
                @params += String.Format("{0} {1}",parameter.Type,parameter.Name);

                if (this.Parameters.Last().Name != parameter.Name)
                {
                    @params += ",";
                }
            }

            string accessType = this.AccessType;
            string returnType = this.ReturnType;
            string name = this.Name;

            if(this.IsAsync)
            {
                accessType = this.AccessType + " async";
                returnType = $"Task<{this.ReturnType}>";
                name = name + "Async";
            }

            StringBuilder builder = new StringBuilder($"{accessType} {returnType} {name} ({@params})");         

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
    }
}
