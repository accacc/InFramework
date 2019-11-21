using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update.Items
{


    public class HandlerGenerator : CSUpdateGenerator, IGenerateItem
    {

        public HandlerGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.UpdateHandler;
        }


        public void Execute()
        {
            CSClass @class = new CSClass();
            @class.Name = GetDataUpdateCommandClassName();
            @class.NameSpace = this.Context.nameSpaceName + ".Commands.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Contract.Commands");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{this.Context.nameSpaceName}.Persistence.EF.Commands");

            @class.InheritedInterfaces.Add($"ICommandHandlerAsync<{this.Context.className}Command>");

            var repositoryProperty = new CSProperty("private", "dataCommand", false);
            repositoryProperty.PropertyTypeString = GetDataUpdateCommandIntarfaceName();
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "dataCommand", Type = GetDataUpdateCommandIntarfaceName() });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.dataCommand = dataCommand;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("Handle", "void", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = $"{this.Context.className}Command" });
            handleMethod.Body += $"await this.dataCommand.ExecuteAsync(command);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            this.Context.fileSystem.FormatCode(@class.GenerateCode(), "cs");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, GetDataUpdateCommandClassName(), vsFile.FileExtension);

        }
    }

}