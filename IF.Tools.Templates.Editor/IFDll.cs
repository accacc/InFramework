using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.Templates.Editor
{
    public class IFNugetPackage
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }        

    }    

    public class IFProject
    {

        [Key]
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int IFProjectTemplateId { get; set; }

        public string Sdk { get; set; }        

        public IFProjectTemplate ProjectTemplate { get; set; }

        public List<IFNugetPackage> NugetPackages { get; set; }


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
