using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Mapper
{
    public interface IMapperService: IBaseService
    {
        T MapTo<T>(object entity) where T : class;
        List<T> MapTo<T>(IEnumerable<object> entityList) where T : class;
    }
}
