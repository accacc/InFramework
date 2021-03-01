using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{

    public interface ITreeClass<T> : IUniqueable
    {
        IList<T> Childs { get; set; }

        int? ParentId { get; set; }

        T Parent { get; set; }

        bool Selected { get; set; }

        int? SortOrder { get; set; }

        int Level { get; set; }
    }
    public class TreeDto<T> : BaseDto, ITreeClass<T>
    {
        public IList<T> Childs { get; set; }

        public int? ParentId { get; set; }

        public T Parent { get; set; }

        public bool Selected { get; set; }

        public int? SortOrder { get; set; }

        public int Level { get; set; }




    }
}
