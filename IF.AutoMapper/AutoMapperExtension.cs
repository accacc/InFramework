using IF.Core.Mapper;
using System.Collections.Generic;

namespace IF.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static IMapperService Instance;

        public static T MapTo<T>(this object entity) where T : class
        {
            return Instance.MapTo<T>(entity);

        }


        public static List<T> MapTo<T>(this IEnumerable<object> entityList) where T : class
        {
            return Instance.MapTo<T>(entityList);
        }


    }
}
