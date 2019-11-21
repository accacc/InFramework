using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class DataHandlerClass: CSListGenerator, IGenerateItem
    {

        public DataHandlerClass(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = VSFileType.DataHandler, Path = "" });
            this.FileType = VSFileType.ListDataHandler;
        }

        public void Execute()
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataQueryClassName();
            @class.NameSpace = this.Context.nameSpaceName + ".Persistence.EF.Queries";

            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Queries");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Models");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"IF.Persistence");
            @class.Usings.Add($"System.Linq");
            @class.Usings.Add($"Microsoft.EntityFrameworkCore");


            @class.InheritedInterfaces.Add(GetDataQueryIntarfaceName());

            var repositoryProperty = new CSProperty("private", "repository", false);
            repositoryProperty.PropertyTypeString = "IRepository";
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "repository", Type = "IRepository" });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.repository = repository;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);


            CSMethod handleMethod = new CSMethod("Get", this.Context.className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = this.Context.className + "Request" });
            handleMethod.Body += $"var data = await this.repository.GetQuery<{this.Context.classType.Name}>()" + Environment.NewLine;
            handleMethod.Body += $".Select(x => new {this.Context.className}Dto" + Environment.NewLine;
            handleMethod.Body += $"{{" + Environment.NewLine;

            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                handleMethod.Body += $"{classProperty.Name} = x.{classProperty.Name}," + Environment.NewLine;
            }

            handleMethod.Body += $"}}).ToListAsync();" + Environment.NewLine;

            handleMethod.Body += $"return new {this.Context.className}Response {{ Data = data }};" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            this.Context.fileSystem.FormatCode(@class.GenerateCode(), "cs");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, GetDataQueryClassName(), vsFile.FileExtension);
        }

       
    }
}
