using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.MongoDb
{
    public interface IMongoDbGenericRepository
    {
        //IMongoCollection<T> GetQuery<T>();

        //IMongoCollection<T> GetQuery<T>(string tableName);

        //IMongoCollection<T> GetQuery<T>(string tableName, string database);

        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter);       

        Task<IEnumerable<T>> GetAllAsync<T>(string tableName);        

        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter, string tableName);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetAsync<T>(Guid id) where T : IIFSystemTable;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> filter);
        Task<T> GetAsync<T>(Expression<Func<T, bool>> filter, string tableName);


        Task AddAsync<T>(T item);

        Task AddAsync<T>(T item, string tableName);

        void Add<T>(T item, string tableName);

        void Add<T>(T item);

        Task DropDatabaseAsync(string dbName);

    }
}
