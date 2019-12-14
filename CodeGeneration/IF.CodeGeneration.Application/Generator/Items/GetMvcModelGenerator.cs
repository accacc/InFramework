using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{
    public class GetMvcModelGenerator :  ApplicationCodeGenerateItem
    {
        public GetMvcModelGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.GetMvcModels;
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
