using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class MvcModelGenerator : CSListGenerator, IGenerateItem
    {

        public MvcModelGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = VSFileType.MvcModel, Path = "" });
            this.FileType = VSFileType.ListMvcModel;
        }

        public void Execute()
        {
            CSClass gridClass = GenerateClass("GridModel");
            gridClass.NameSpace = this.Context.nameSpaceName + ".Models";
            this.Context.fileSystem.FormatCode(gridClass.GenerateCode(), "cs");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }

        
    }
}
