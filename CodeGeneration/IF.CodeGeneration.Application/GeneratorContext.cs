using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Core;
using IF.Tools.CodeGenerator.VsAutomation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application
{
    public class ApplicationCodeGeneratorContext
    {
        internal readonly FileSystemCodeFormatProvider fileSystem;
        internal ClassTree classTree;
        internal Type classType;
        internal readonly VsManager VsManager;
        internal string BaseCommandName = "BaseCommand";
        internal string className;
        internal string nameSpaceName;


        public List<IFVsFile> Files { get; set; }

        public string RepositoryName { get; set; }
        public string ControllerName { get; set; }

        public string Title { get; set; }

        public ApplicationCodeGeneratorContext(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType, VsManager vsManager)
        {
            this.fileSystem = fileSystem;
            this.className = className;
            this.nameSpaceName = nameSpaceName;
            this.classTree = classTree;
            this.classType = classType;
            this.VsManager = vsManager;
            this.Files = new List<IFVsFile>();
        }
    }
}
