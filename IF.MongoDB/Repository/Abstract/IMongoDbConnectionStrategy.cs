using IF.Core.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Repository.Abstract
{
    public interface IMongoDbConnectionStrategy
    {
        IMongoClient GetConnection();

        MongoConnectionSettings ConnectionSettings { get; set; }
    }
}
