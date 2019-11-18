using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    class HandlerClassGenerator :  CSListGenerator, IGenerateItem
    {
        public HandlerClassGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.Handler, Path = "" });
            this.FileType = ListFileType.Handler;
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

            var repositoryProperty = new CSProperty("private", "query", false);
            repositoryProperty.PropertyTypeString = GetDataQueryIntarfaceName();
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "query", Type = GetDataQueryIntarfaceName() });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.query = query;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("Handle", this.Context.className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = this.Context.className + "Request" });
            handleMethod.Body += $"return await this.query.GetAsync(request);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            this.Context.fileSystem.FormatCode(@class.GenerateCode(), "cs");

            VsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, this.Context.className + "Handler", vsFile.FileExtension);
        }

     
    }
}
