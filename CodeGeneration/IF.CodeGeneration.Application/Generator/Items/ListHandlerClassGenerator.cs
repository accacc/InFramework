using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    class ListHandlerClassGenerator :  CSListGenerator, IGenerateItem
    {
        public ListHandlerClassGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = VSFileType.Handler, Path = "" });
            this.FileType = VSFileType.ListHandler;
        }
        public void Execute()
        {
            CSClass @class = new CSClass();
            @class.Name = this.Context.className + "Handler";
            @class.NameSpace = this.Context.nameSpaceName + ".Queries.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Queries");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Repositories");
            

            @class.InheritedInterfaces.Add($"IQueryHandlerAsync<{this.Context.className}Request, {this.Context.className}Response>");

            var repositoryProperty = new CSProperty("private", "repository", false);
            repositoryProperty.PropertyTypeString =$"I{this.Context.ControllerName}Repository";
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "repository", Type = $"I{this.Context.ControllerName}Repository" });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.repository = repository;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("HandleAsync", this.Context.className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = this.Context.className + "Request" });

            handleMethod.Body += $"{this.Context.className}Response response = new {this.Context.className}Response();" + Environment.NewLine;
            handleMethod.Body += $"response.Data =   await this.repository.Get{this.Context.className}();" + Environment.NewLine;
            handleMethod.Body += $"return response;" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            this.Context.fileSystem.FormatCode(@class.GenerateCode(), "cs");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, this.Context.className + "Handler", vsFile.FileExtension);
        }

     
    }
}
