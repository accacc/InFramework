using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class MvcModelGenerator : CSListGenerator, IGenerateItem
    {

        public MvcModelGenerator(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType)
            : base(fileSystem, className, nameSpaceName, classTree, classType)
        {
            this.File = new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.MvcModel, Path = "" };
        }

        public void Execute()
        {
            CSClass gridClass = GenerateClass("GridModel");
            gridClass.NameSpace = nameSpaceName + "Models";
            fileSystem.FormatCode(gridClass.GenerateCode(), "cs");
        }

        
    }
}
