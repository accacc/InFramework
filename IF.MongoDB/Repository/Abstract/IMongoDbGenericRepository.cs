using IF.Core.Log;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Abstract
{
    public interface IMongoDbGenericRepository
    {
        IMongoCollection<T> GetQuery<T>();

        IMongoCollection<T> GetQuery<T>(string tableName);

        IMongoCollection<T> GetQuery<T>(string tableName, string database);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetAsync<T>(Guid id) where T : IIFSystemTable;


        Task AddAsync<T>(T item);

        void Add<T>(T item);

        Task DropDatabaseAsync(string dbName);

    }
}
