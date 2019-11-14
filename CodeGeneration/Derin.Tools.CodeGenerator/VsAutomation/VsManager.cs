using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.CodeGenerator.VsAutomation
{
    public class VsManager
    {
        private readonly string solutionName;
        private readonly string solutionPath;
        private readonly string basePath;
        public VsManager(string solutionName,string solutionPath,string path)
        {
            this.solutionName = solutionName;
            this.solutionPath = solutionPath;
            this.basePath = path;
        }

        public void AddVisualStudio(string projectName, string directory, string itemName)
        {
            var p = new Microsoft.Build.Evaluation.Project(GetProjectFilePath(projectName));
            p.AddItem("Folder", $@"{GetProjectPath(projectName)}\{directory}");
            p.AddItem("Compile", $@"{GetProjectPath(projectName)}\{directory}\{itemName}.cs");
            p.Save();

            File.Copy($@"{basePath}\{itemName}.cs", $@"{GetProjectPath(projectName)}\{directory}\{itemName}.cs", true);
        }

        private string GetProjectFilePath(string projectName)
        {
            return $@"{GetProjectPath(projectName)}\{solutionName}.{projectName}.csproj";
        }


        private string GetProjectPath(string projectPath)
        {
            return $@"{solutionPath}\{solutionName}\{solutionName}.{projectPath}";
        }
    }
}
