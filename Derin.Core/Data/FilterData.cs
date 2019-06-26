using System.Collections.Generic;
using System.ComponentModel;

namespace Derin.Core.Data
{
    public class FilterData
    {
        public FilterData()
        {
            this.sortDescriptor = new Dictionary<string, ListSortDirection>();
        }

        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public Dictionary<string, ListSortDirection>  sortDescriptor { get; set; }

       
    }
}
