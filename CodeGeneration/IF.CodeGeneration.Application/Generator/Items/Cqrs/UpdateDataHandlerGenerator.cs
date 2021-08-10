using IF.CodeGeneration.Language.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update.Items
{
    public class UpdateDataHandlerGenerator :  ApplicationCodeGenerateItem
    {

        public UpdateDataHandlerGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.UpdateDataHandler;
        }
        public override void Execute()
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataInsertCommandClassName();
            @class.NameSpace = this.Context.nameSpaceName + ".Persistence.EF.Commands";

            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Commands");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Models");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"IF.Persistence");
            @class.Usings.Add($"IF.Core.Exception");
            @class.Usings.Add($"System.Linq");
            @class.Usings.Add($"Microsoft.EntityFrameworkCore");


            @class.InheritedInterfaces.Add(GetDataUpdateCommandIntarfaceName());

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


            CSMethod handleMethod = new CSMethod("Execute", "void", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = this.Context.className + "Command" });

            handleMethod.Body += $"var entity = await this.repository.GetQuery<{this.Context.classType.Name}>().SingleOrDefaultAsync(k => k.Id == command.Data.Id);" + Environment.NewLine + Environment.NewLine;
            handleMethod.Body += $"if (entity == null){{ throw new BusinessException(\"{this.Context.className} : No such entity exists\");}}" + Environment.NewLine +  Environment.NewLine;


            foreach (var property in this.Context.classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(property.Name.Split('\\')[2]);
                handleMethod.Body += $"entity.{classProperty.Name} = command.Data.{classProperty.Name};" + Environment.NewLine;
            }

            
            handleMethod.Body += $"this.repository.Update(entity);" + Environment.NewLine;
            handleMethod.Body += $"await this.repository.UnitOfWork.SaveChangesAsync();" + Environment.NewLine;
            //handleMethod.Body += $"command.Data.Id = entity.Id;" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            this.Context.fileSystem.FormatCode(@class.GenerateCode() ,"cs", "", "");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddFile(vsFile.ProjectName, vsFile.Path, this.Context.className + "Handler", vsFile.FileExtension);
        }
    }

}
