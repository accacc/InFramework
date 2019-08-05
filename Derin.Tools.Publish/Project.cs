using IF.Core.Data;
using System.Collections.Generic;

namespace IF.Tools.Publish
{
    public class Project:BaseJsonDto
    {


        public Project()
        {
            this.Modules = new List<ProjectModule>();
            this.FrameworkModules = new List<ProjectModule>();
            this.ChildProjects = new List<Project>();
            this.ParentProjects = new List<Project>();
        }


        public List<Project>ParentProjects { get; set; }
        public List<Project> ChildProjects { get; set; }

        public List<ProjectModule> Modules { get; set; }

        public List<ProjectModule> FrameworkModules { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public ProjectType ProjectType { get; set; }

        public bool IsFrameworkProject { get; set; }

        public string ProjectPath { get; set; }
    }
}
