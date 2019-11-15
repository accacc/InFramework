using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class DataHandlerClass: CSListGenerator, IGenerateItem
    {

        public DataHandlerClass(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType)
            : base(fileSystem, className, nameSpaceName, classTree, classType)
        {
            this.File = new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.DataHandler, Path = "" };
        }

        public void Execute()
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataQueryClassName();
            @class.NameSpace = nameSpaceName + ".Persistence.EF.Queries";

            @class.Usings.Add($"{nameSpaceName}.Contract.Queries");
            @class.Usings.Add($"{nameSpaceName}.Persistence.EF.Models");
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


            CSMethod handleMethod = new CSMethod("Get", className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = className + "Request" });
            handleMethod.Body += $"var data = await this.repository.GetQuery<{classType.Name}>()" + Environment.NewLine;
            handleMethod.Body += $".Select(x => new {className}Dto" + Environment.NewLine;
            handleMethod.Body += $"{{" + Environment.NewLine;

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                handleMethod.Body += $"{classProperty.Name} = x.{classProperty.Name}," + Environment.NewLine;
            }

            handleMethod.Body += $"}}).ToListAsync();" + Environment.NewLine;

            handleMethod.Body += $"return new {className}Response {{ Data = data }};" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");
        }

       
    }
}
