using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.Templates.Editor
{
    public class IFDll
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<IFDll> Dependencies { get; set; }
    }

    public class IFProject
    {

        [Key]
        public int Id { get; set; }
        //public List<IFDll> Dlls { get; set; }
        public string Name { get; set; }

        public int IFProjectTemplateId { get; set; }

        public IFProjectTemplate ProjectTemplate { get; set; }

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
