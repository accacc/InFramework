using System.Collections.Generic;

namespace IF.Core.Data
{
    public interface IHierarchical<T>
    {
        T Parent { get; set; }
        ICollection<T> Childrens { get; set; }
    }
}
