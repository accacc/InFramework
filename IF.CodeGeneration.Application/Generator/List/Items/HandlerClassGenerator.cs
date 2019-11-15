using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    class HandlerClassGenerator :  CSListGenerator, IGenerateItem
    {
        public HandlerClassGenerator(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType)
            : base(fileSystem, className, nameSpaceName, classTree, classType)
        {
            this.File = new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.Handler, Path = "" };
        }
        public void Execute()
        {
            CSClass @class = new CSClass();
            @class.Name = className + "Handler";
            @class.NameSpace = nameSpaceName + ".Queries.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{nameSpaceName}.Contract.Queries");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{nameSpaceName}.Persistence.EF.Queries");

            @class.InheritedInterfaces.Add($"IQueryHandlerAsync<{className}Request, {className}Response>");

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

            CSMethod handleMethod = new CSMethod("Handle", className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = className + "Request" });
            handleMethod.Body += $"return await this.query.GetAsync(request);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");

        }

     
    }
}
