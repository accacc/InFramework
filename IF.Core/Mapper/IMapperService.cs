using System.Collections.Generic;

namespace IF.Core.Mapper
{
    public interface IMapperService
    {
        T MapTo<T>(object entity) where T : class;
        List<T> MapTo<T>(IEnumerable<object> entityList) where T : class;
    }
}
