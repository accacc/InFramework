using IF.Core.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Repository.Abstract
{

    public class MongoDbPerServiceConnectionStrategy : IMongoDbConnectionStrategy
    {
        private readonly MongoClient client = null;

        public MongoConnectionSettings ConnectionSettings { get; set; }
        public MongoDbPerServiceConnectionStrategy(MongoConnectionSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            this.ConnectionSettings = settings;
        }
        public IMongoClient GetConnection()
        {
            return this.client;
        }
    }
}
