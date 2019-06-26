using Derin.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    public interface IDbPagingQuery<T>
    {
        PagedListResponse<T> GetPageList(BasePagingRequest param);
    }
}
