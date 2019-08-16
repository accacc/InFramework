using IF.MongoDB.Repository.Abstract;
using IF.Template.Contract.Interfaces.NoSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.NoSql.MongoDB
{
    public class TestMongoRepository: MongoDbGenericRepository, IIFTemplateMongoRepository
    {
        public TestMongoRepository(IMongoDbConnectionStrategy connectionStrategy):base(connectionStrategy)
        {

        }
    }
}
