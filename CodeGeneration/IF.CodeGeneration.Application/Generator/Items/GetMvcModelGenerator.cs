using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{
    public class GetMvcModelGenerator : CSGetGenerator, IGenerateItem
    {
        public GetMvcModelGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.GetMvcModels;
        }

        public void Execute()
        {
            CSClass gridClass = GenerateClass("Model");

            gridClass.NameSpace = this.Context.nameSpaceName + ".Models";

            IFVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(gridClass.GenerateCode(), vsFile.FileExtension);


            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }
    }

    
}
