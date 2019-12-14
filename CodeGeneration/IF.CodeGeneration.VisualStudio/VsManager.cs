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

        public void AddVisualStudio(string projectName, string directory, string itemName,string fileExtension)
        {
            //var p = new Microsoft.Build.Evaluation.Project(GetProjectFilePath(projectName));



            //if (!Directory.Exists($@"{GetProjectPath(projectName)}/{directory}"))
            //{
            //    p.AddItem("Folder", $@"{GetProjectPath(projectName)}/{directory}");
            //    Directory.CreateDirectory($@"{GetProjectPath(projectName)}\{directory}");
            //}

            //if (!File.Exists($@"{GetProjectPath(projectName)}/{directory}/{itemName}.{fileExtension}"))
            //{
            //    p.AddItem("Compile", $@"{GetProjectPath(projectName)}/{directory}/{itemName}.{fileExtension}");
            //}

            //p.Save();

            //File.Copy($@"{basePath}\{itemName}.{fileExtension}", $@"{GetProjectPath(projectName)}/{directory}/{itemName}.{fileExtension}", true);

            //p.ProjectCollection.UnloadProject(p);
        }

        public string GetProjectFilePath(string projectName)
        {
            return $@"{GetProjectPath(projectName)}/{solutionName}.{projectName}.csproj";
        }


        public string GetProjectPath(string projectPath)
        {
            return $@"{solutionPath}/{solutionName}/{solutionName}.{projectPath}";
        }
    }
}
