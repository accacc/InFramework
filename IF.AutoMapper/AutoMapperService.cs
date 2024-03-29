﻿using AutoMapper;
using IF.Core.Mapper;
using System;
using System.Collections.Generic;

namespace IF.AutoMapper
{
    public class AutoMapperService : IMapperService
    {

        IMapper Instance;
        public AutoMapperService(IMapper Instance)
        {
            this.Instance = Instance;
        }

        

        public List<T> MapTo<T>(IEnumerable<object> entityList) where T : class
        {
            //if (entityList == null) return null;

            return (List<T>)Instance.Map(entityList, entityList.GetType(), typeof(List<T>));
        }

        public T MapTo<T>(object entity) where T : class
        {
            //if (entity == null) return null;
            return (T)Instance.Map(entity, entity.GetType(), typeof(T));
        }
    }
}
