using IF.Core.Data;
using IF.Core.MongoDb;
using IF.Core.Sms;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using IF.MongoDB.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{

    public class MongoSmsLogRepository : MongoDbGenericRepository,IMongoSmsLogRepository
    {
        

        //public MongoSmsLogRepository(MongoConnectionSettings settings) : base(settings)
        //{
            
        //}

        public MongoSmsLogRepository(IMongoDbConnectionStrategy connectionStrategy) : base(connectionStrategy)
        {

        }


        public async Task<PagedListResponse<ISmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<SmsLog>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.Date, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.Date, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(number))
            {
                var userFilter = filterBuilder.Eq(i => i.Number, number);
                filter = filter & userFilter;
            }


            var fields = Builders<SmsLog>.Projection.Exclude("_id");


            var list = await this.GetQuery<SmsLog>().Find(filter).Project<ISmsLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.Date).ToListAsync();
            var count = await this.GetQuery<SmsLog>().CountDocumentsAsync(filter);


            return new PagedListResponse<ISmsLog>(list, PageNumber, PageSize, count);
        }


        public async Task<PagedListResponse<SmsBulkOneToManyOperation>> GetPaginatedSmsBulkOneToManyOperationAsync(DateTime BeginDate, DateTime EndDate, string bulkName, int PageNumber = 0, int PageSize = 50)

        {

            var filterBuilder = Builders<SmsBulkOneToManyOperationMongoDb>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.CreatedDate, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.CreatedDate, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(bulkName))
            {
                var userFilter = filterBuilder.Eq(i => i.BulkName, bulkName);
                filter = filter & userFilter;
            }


            var fields = Builders<SmsBulkOneToManyOperationMongoDb>.Projection.Exclude("_id");


            var list = await this.GetQuery<SmsBulkOneToManyOperationMongoDb>(nameof(SmsBulkOneToManyOperation)).Find(filter).Project<SmsBulkOneToManyOperation>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.CreatedDate).ToListAsync();
            var count = await this.GetQuery<SmsBulkOneToManyOperationMongoDb>(nameof(SmsBulkOneToManyOperation)).CountDocumentsAsync(filter);


            return new PagedListResponse<SmsBulkOneToManyOperation>(list, PageNumber, PageSize, count);
        }


        public async Task<List<SmsBatchResult>> GetSmsBulkResultOneToManyList(string bulkName)
        {            

            var fields = Builders<SmsBatchResultOneToManyMongoDb>.Projection.Exclude("_id");

            var list = await this.GetQuery<SmsBatchResultOneToManyMongoDb>(nameof(SmsBatchResult),bulkName).Find(_=>true).Project<SmsBatchResult>(fields).SortByDescending(s => s.CreatedDate).ToListAsync();

            return list;

        }

    }
}
