using System.Collections.Generic;

namespace Derin.Core.Data
{
    public interface IHierarchical<T>
    {
        T Parent { get; set; }
        ICollection<T> Childrens { get; set; }
    }
}
