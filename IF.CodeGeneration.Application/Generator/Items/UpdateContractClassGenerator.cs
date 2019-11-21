using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update.Items
{
    public class UpdateContractClassGenerator : CSUpdateGenerator, IGenerateItem
    {

        public UpdateContractClassGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.UpdateContractClass;
        }

        public void Execute()
        {

            CSClass @class = new CSClass();
            @class.Name = this.Context.className + "Dto";
            //@class.NameSpace = namespaceName + ".Contract.Queries";
            @class.Properties = new List<CSProperty>();

            foreach (var property in this.Context.classTree.Childs)
            {
                @class.Properties.Add(GetClassProperty(property.Name.Split('\\')[2]));
            }



            CSClass commandClass = new CSClass();
            commandClass.BaseClass = this.Context.BaseCommandName;
            commandClass.Name = this.Context.className + "Command";
            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = $"{this.Context.className}Dto";
            commandClass.Properties.Add(dtoProperty);



            CSInterface @interface = new CSInterface();
            @interface.Name = GetDataInsertCommandIntarfaceName();
            @interface.InheritedInterfaces.Add($"IDataUpdateCommandAsync<{this.Context.className}Command>");

            string classes = "";
            classes += "using IF.Core.Data;";
            classes += Environment.NewLine;
            classes += "using System.Collections.Generic;";
            classes += Environment.NewLine;
            classes += Environment.NewLine;
            classes += "namespace " + this.Context.nameSpaceName + ".Contract.Commands";
            classes += Environment.NewLine;
            classes += "{";
            classes += Environment.NewLine;
            classes += @class.GenerateCode().Template + Environment.NewLine + commandClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            this.Context.fileSystem.FormatCode(classes, "cs", this.Context.className);

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, this.Context.className, vsFile.FileExtension);

        }
    }
}