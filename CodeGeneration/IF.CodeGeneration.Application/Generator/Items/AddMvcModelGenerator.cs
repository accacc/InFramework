using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Add.Items
{
    public class AddMvcModelGenerator :  ApplicationCodeGenerateItem
    {
        public AddMvcModelGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.AddMvcModels;
        }

        public override void Execute()
        {
            CSClass gridClass = GenerateClass("Model");
            gridClass.NameSpace = this.Context.nameSpaceName + ".Models";

            IFVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(gridClass.GenerateCode(), vsFile.FileExtension);


            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }
    }

    
}
