using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.Templates.Editor
{

    public class IFProjectNugetPackage
    {
        [Key]
        public int Id { get; set; }
        public int IFNugetPackageId { get; set; }
        public int IFProjectId { get; set; }

        public IFNugetPackage NugetPackage { get; set; }

        public IFProject Project { get; set; }
    }

    public class IFNugetPackage
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<IFProjectNugetPackage> IFProjectNugetPackages { get; set; }

    }    

    public class IFProject
    {

        [Key]
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int IFProjectTemplateId { get; set; }

        public string Sdk { get; set; }


        public IFProjectTemplate ProjectTemplate { get; set; }

        public List<IFProjectNugetPackage> IFProjectNugetPackages { get; set; }


    }

    public class IFProjectTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SolutionName { get; set; }

        public string Code { get; set; }
        public List<IFProject> ProjectList { get; set; }
    }

    public static class TemplateManage
    {

        public static void Init()
        {
            

        }

        

    }




}
