using IF.Core.Log;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Abstract
{
    public abstract class GenericRepository: IRepository
    {
        private readonly IMongoDatabase _database = null;
        private readonly MongoClient _client = null;

        public GenericRepository(string cnnString, string database)
        {
            _client = new MongoClient(cnnString);
            if (_client != null)
                _database = _client.GetDatabase(database);
        }

        public IMongoCollection<T> GetQuery<T>()
        {
            return _database.GetCollection<T>(nameof(T));
        }

        public IMongoCollection<T> GetQuery<T>(string tableName)
        {
            return _database.GetCollection<T>(tableName);
        }

        public IMongoCollection<T> GetQuery<T>(string tableName,string database)
        {
            var db = _client.GetDatabase(database);
            return db.GetCollection<T>(tableName);
        }


        public async Task<IEnumerable<T>> GetAllLogsAsync<T>()
        {
            return await this.GetQuery<T>().Find(_ => true).ToListAsync();

        }

        public async Task<T> GetLogAsync<T>(Guid id) where T : IIFSystemTable
        {

            return await this.GetQuery<T>().Find(log => log.UniqueId == id).SingleOrDefaultAsync();

        }

        public async Task AddLogAsync<T>(T item)
        {
            await this.GetQuery<T>().InsertOneAsync(item);
        }

        public void AddLog<T>(T item)
        {
            this.GetQuery<T>().InsertOne(item);
        }
    }

}
