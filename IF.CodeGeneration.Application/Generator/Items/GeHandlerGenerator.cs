﻿using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{


    public class GeHandlerGenerator : CSGetGenerator, IGenerateItem
    {

        public GeHandlerGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.GetHandler;
        }


        public void Execute()
        {
            CSClass @class = new CSClass();
            @class.Name = this.Context.className + "Handler";
            @class.NameSpace = this.Context.nameSpaceName + ".Queries.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Queries");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Queries");

            @class.InheritedInterfaces.Add($"IQueryHandlerAsync<{this.Context.className}Request, {this.Context.className}Response>");

            var repositoryProperty = new CSProperty("private", "repository", false);
            repositoryProperty.PropertyTypeString = $"I{this.Context.classType.Name}Repository";
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "repository", Type = $"I{this.Context.classType.Name}Repository" });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.repository = repository;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("Handle", this.Context.className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = this.Context.className + "Request" });
            handleMethod.Body += $"await this.repository.{this.Context.className}(request);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);
            this.Context.fileSystem.FormatCode(@class.GenerateCode(), "cs");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path,vsFile.FileName, vsFile.FileExtension);

        }
    }

}