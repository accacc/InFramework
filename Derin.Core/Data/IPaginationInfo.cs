using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Data
{
    public interface IPaginationInfo
    {
        int PageNumber { get; }
        int PageSize { get; }
    }
}
