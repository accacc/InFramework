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

        public GenericRepository(string cnnString, string database)
        {
            var client = new MongoClient(cnnString);
            if (client != null)
                _database = client.GetDatabase(database);
        }

        public IMongoCollection<T> GetQuery<T>()
        {
            return _database.GetCollection<T>(nameof(T));
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
