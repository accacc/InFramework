using IF.CodeGeneration.Application.Generator.Get;
using IF.CodeGeneration.CSharp;

using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{
    public class GetDataHandlerGenerator : ApplicationCodeGenerateItem
    {

        public GetDataHandlerGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.GetDataHandler;
        }
        public override void Execute()
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataQueryClassName();
            @class.NameSpace = this.Context.nameSpaceName + ".Persistence.EF.Queries";

            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Queries");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Models");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"IF.Persistence");
            @class.Usings.Add($"System.Linq");
            @class.Usings.Add("IF.Core.Exception");
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


            handleMethod.Body += $"var entity = await this.repository.GetQuery<{this.Context.classType.Name}>()" + Environment.NewLine;



            handleMethod.Body += $".Select(x => new {this.Context.className}Dto" + Environment.NewLine;
            handleMethod.Body += $"{{" + Environment.NewLine;

            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                handleMethod.Body += $"{classProperty.Name} = x.{classProperty.Name}," + Environment.NewLine;
            }

            handleMethod.Body += $"}}).SingleOrDefaultAsync(k => k.Id == request.Id);" + Environment.NewLine + Environment.NewLine;

            handleMethod.Body += $"if (entity == null){{ throw new BusinessException(\"{this.Context.className} : No such entity exists\");}}" + Environment.NewLine + Environment.NewLine;

            handleMethod.Body += $"return new {this.Context.className}Response {{ Data = entity }};" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            IFVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(@class.GenerateCode(), vsFile.FileExtension,"","");


            this.Context.VsManager.AddFile(vsFile.ProjectName, vsFile.Path, GetDataQueryClassName(), vsFile.FileExtension);
        }
    }

}
