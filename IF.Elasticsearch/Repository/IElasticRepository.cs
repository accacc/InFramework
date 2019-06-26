using IF.Elasticsearch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Elasticsearch.Repository
{
    public interface IElasticRepository
    {
        Guid Save<T>(T entity) where T : class, IBaseDocument;
        T Get<T>(Guid id) where T : class;
        void Update<T>(T entity) where T : class;
        bool Delete<T>(Guid id) where T : class;
        IEnumerable<T> All<T>() where T : class;

        void CheckIndex<T>() where T : class;
    }
}
