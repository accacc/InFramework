using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{
    public class MvcModelGenerator : CSGetGenerator, IGenerateItem
    {
        public MvcModelGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = GetFileType.MvcModels;
        }

        public void Execute()
        {
            CSClass gridClass = GenerateClass("Model");

            gridClass.NameSpace = this.Context.nameSpaceName + ".Models";

            GetVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(gridClass.GenerateCode(), vsFile.FileExtension);


            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }
    }

    
}
