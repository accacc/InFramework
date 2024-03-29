﻿using System;
using System.Collections.Generic;
using System.Text;
using IF.Core.MongoDb;
using MongoDB.Driver;

namespace IF.MongoDB.Repository.Abstract
{
    public class MongoDbSingleConnectionStrategy : IMongoDbConnectionStrategy
    {
        private readonly MongoClient client = null;

        public MongoConnectionSettings ConnectionSettings { get; set; }
        public MongoDbSingleConnectionStrategy(MongoConnectionSettings settings)
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
