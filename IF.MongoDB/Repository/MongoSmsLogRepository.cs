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

        public async Task<PagedListResponse<SmsBulkManyToManyOperation>> GetPaginatedSmsBulkManyToManyOperationAsync(DateTime BeginDate, DateTime EndDate, string bulkName, int PageNumber, int PageSize)
        {
            var filterBuilder = Builders<SmsBulkManyToManyOperationMongoDb>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.CreatedDate, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.CreatedDate, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(bulkName))
            {
                var userFilter = filterBuilder.Eq(i => i.BulkName, bulkName);
                filter = filter & userFilter;
            }


            var fields = Builders<SmsBulkManyToManyOperationMongoDb>.Projection.Exclude("_id");


            var list = await this.GetQuery<SmsBulkManyToManyOperationMongoDb>(nameof(SmsBulkManyToManyOperation)).Find(filter).Project<SmsBulkManyToManyOperation>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.CreatedDate).ToListAsync();
            var count = await this.GetQuery<SmsBulkManyToManyOperationMongoDb>(nameof(SmsBulkManyToManyOperation)).CountDocumentsAsync(filter);


            return new PagedListResponse<SmsBulkManyToManyOperation>(list, PageNumber, PageSize, count);
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

        public async Task<PagedListResponse<SmsModel>> GetPaginatedSmsList(DateTime BeginDate, DateTime EndDate, string bulkName, string number, SmsState? smsState, int pageNumber, int pageSize)
        {
            var filterBuilder = Builders<SmsModel>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.SentDate, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.SentDate, new BsonDateTime(end));



            if (smsState.HasValue)
            {
                var stateFilter = filterBuilder.Eq(x => x.State, smsState);
            }

            if (!String.IsNullOrWhiteSpace(number))
            {
                var userFilter = filterBuilder.Eq(i => i.Number, number);
                filter = filter & userFilter;
            }


            var fields = Builders<SmsModel>.Projection.Exclude("_id");


            var list = await this.GetQuery<SmsModel>("SMS",bulkName).Find(filter).Project<SmsModel>(fields).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
            var count = await this.GetQuery<SmsModel>("SMS",bulkName).CountDocumentsAsync(filter);


            return new PagedListResponse<SmsModel>(list, pageNumber, pageSize, count);
        }

        public async Task<List<SmsBatchResult>> GetSmsBulkResultManyToManyList(string bulkName)
        {
            var fields = Builders<SmsBatchResultManyToManyMongoDb>.Projection.Exclude("_id");

            var list = await this.GetQuery<SmsBatchResultManyToManyMongoDb>(nameof(SmsBatchResult), bulkName).Find(_ => true).Project<SmsBatchResult>(fields).SortByDescending(s => s.CreatedDate).ToListAsync();

            return list;
        }

        public async Task<List<SmsBatchResult>> GetSmsBulkResultOneToManyList(string bulkName)
        {            

            var fields = Builders<SmsBatchResultOneToManyMongoDb>.Projection.Exclude("_id");

            var list = await this.GetQuery<SmsBatchResultOneToManyMongoDb>(nameof(SmsBatchResult),bulkName).Find(_=>true).Project<SmsBatchResult>(fields).SortByDescending(s => s.CreatedDate).ToListAsync();

            return list;

        }

    }
}
