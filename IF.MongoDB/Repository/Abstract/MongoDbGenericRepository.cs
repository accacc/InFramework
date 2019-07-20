using IF.Core.Log;
using IF.Core.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Abstract
{
    public abstract class MongoDbGenericRepository: IMongoDbGenericRepository
    {
        private readonly IMongoDatabase database = null;
        private readonly IMongoClient client = null;
        private readonly IMongoDbConnectionStrategy connectionStrategy;

        public MongoDbGenericRepository(IMongoDbConnectionStrategy connectionStrategy)
        {
            this.connectionStrategy = connectionStrategy;
            this.client = this.connectionStrategy.GetConnection();
            this.database = this.client.GetDatabase(this.connectionStrategy.ConnectionSettings.Database);
        }

       
        public IMongoCollection<T> GetQuery<T>()
        {
            return database.GetCollection<T>(nameof(T));
        }

        public IMongoCollection<T> GetQuery<T>(string tableName)
        {
            return database.GetCollection<T>(tableName);
        }

        public IMongoCollection<T> GetQuery<T>(string tableName,string database)
        {
            var db = this.client.GetDatabase(database);
            return db.GetCollection<T>(tableName);
        }


        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.GetQuery<T>().Find(_ => true).ToListAsync();

        }

        public async Task<T> GetAsync<T>(Guid id) where T : IIFSystemTable
        {

            return await this.GetQuery<T>().Find(log => log.UniqueId == id).SingleOrDefaultAsync();

        }

        public async Task AddAsync<T>(T item)
        {
            await this.GetQuery<T>().InsertOneAsync(item);
        }

        public void Add<T>(T item)
        {
            this.GetQuery<T>().InsertOne(item);
        }

        public async Task DropDatabaseAsync(string dbName)
        {
            await this.client.DropDatabaseAsync(dbName);
        }
    }

}
