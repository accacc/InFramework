using System.Collections.Generic;

namespace IF.CodeGeneration.Application
{


    public class TreeDto<T>
    {
        public TreeDto()
        {
            this.Childs = new List<T>();
        }

        public List<T> Childs { get; set; }
        public int? ParentId { get; set; }
        public int? SortOrder { get; set; }
    }
    public class ClassTree: TreeDto<ClassTree>
    {
        public string Name { get; set; }
    }
}
