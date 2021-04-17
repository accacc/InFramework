using IF.CodeGeneration.CSharp;

using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update.Items
{


    public class CommandHandlerGenerator : ApplicationCodeGenerateItem
    {

        public CommandHandlerGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.CommandHandler;
        }


        public override void Execute()
        {
            CSClass @class = new CSClass();
            @class.Name = $"{this.Context.className}CommandHandler";
            @class.NameSpace = this.Context.nameSpaceName + ".Commands.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Commands");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Services");

            @class.InheritedInterfaces.Add($"ICommandHandlerAsync<{this.Context.className}Command>");

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

            CSMethod handleMethod = new CSMethod("HandleAsync", "void", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = $"{this.Context.className}Command" });
            handleMethod.Body += $"await this.repository.{this.Context.className}(command.Data);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            this.Context.fileSystem.FormatCode(@class.GenerateCode(), "","", "cs");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddFile(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);

        }
    }

}