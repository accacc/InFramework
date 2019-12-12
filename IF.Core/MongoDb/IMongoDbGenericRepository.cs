using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.MongoDb
{
    public interface IMongoDbGenericRepository
    {
        //IMongoCollection<T> GetQuery<T>();

        //IMongoCollection<T> GetQuery<T>(string tableName);

        //IMongoCollection<T> GetQuery<T>(string tableName, string database);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetAsync<T>(Guid id) where T : IIFSystemTable;


        Task AddAsync<T>(T item);

        Task AddAsync<T>(T item, string tableName);

        void Add<T>(T item);

        Task DropDatabaseAsync(string dbName);

    }
}
