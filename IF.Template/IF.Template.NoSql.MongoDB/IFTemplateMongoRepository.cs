using IF.MongoDB.Repository.Abstract;
using IF.Template.Contract.Interfaces.NoSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.NoSql.MongoDB
{
    public class IFTemplateMongoRepository: MongoDbGenericRepository, IIFTemplateMongoRepository
    {
        public IFTemplateMongoRepository(IMongoDbConnectionStrategy connectionStrategy):base(connectionStrategy)
        {

        }
    }
}
