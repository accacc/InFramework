using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Add.Items
{
    public class MvcModelGenerator : CSInsertGenerator, IGenerateItem
    {
        public MvcModelGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.AddMvcModels;
        }

        public void Execute()
        {
            CSClass gridClass = GenerateClass("Model");
            gridClass.NameSpace = this.Context.nameSpaceName + ".Models";

            IVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(gridClass.GenerateCode(), vsFile.FileExtension);


            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }
    }

    
}
