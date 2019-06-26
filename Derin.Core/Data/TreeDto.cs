using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Data
{
    public class TreeDto<T> : BaseDto
    {
        public IEnumerable<T> Childs { get; set; }

        public int? ParentId { get; set; }

        public int? SortOrder { get; set; }




    }
}
