using IF.Batch;
using IF.Core.Extensions;
using IF.Core.Sms;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Notification.MongoDB
{
    public class BatchRepositoryBase: MongoDbGenericRepository, IBatchRepositoryBase
    {
        private readonly string ApplicationDbName;

        public BatchRepositoryBase(IMongoDbConnectionStrategy connectionStrategy) : base(connectionStrategy)
        {
            this.ApplicationDbName = "ApplicationDb";
        }

        public async Task InsertOperation(string BulkName, int SplitBy, int Total, string operationName,string type)
        {


            //var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            //var filterBuilder = Builders<IFBulkOperationMongoDb>.Filter;
            //var batchNameFilter = filterBuilder.Eq(i => i.BulkName, bulk.BulkName);
            //await operationTable.DeleteManyAsync(batchNameFilter);

            IFBulkOperationMongoDb smsBulkOperation = new IFBulkOperationMongoDb();
            smsBulkOperation.BulkName = BulkName;
            smsBulkOperation.SplitBy = SplitBy;
            smsBulkOperation.Total = Total;
            smsBulkOperation.Type = type;
            //smsBulkOperation.EventId = request.EventId;
            smsBulkOperation.Status = IFBulkOperationStatus.Ready;
            //smsBulkOperation.CallBackMessageTemplate = bulk.CallBackMessageTemplate;
            //smsBulkOperation.CallBackNumberId = bulk.CallBackNumberId;
            //smsBulkOperation.CallBackPrefixName = bulk.CallBackPrefixName;
            //smsBulkOperation.StartDate = bulk.StartDate;            
            //smsBulkOperation.EndDate = bulk.EndDate;
            //smsBulkOperation.SenderPrefixName = bulk.SenderPrefixName;

            await this.InsertOperation(smsBulkOperation, operationName);
        }

        public async Task<List<T>> GetBatchItemList<T>(string bulkName, string batchItemName, int batchNumber, int splitBy, int batchCount) where T : IFBatchItemModel
        {
            var batchItemTable = this.GetQuery<T>(batchItemName, bulkName);

            var fields = Builders<T>.Projection.Exclude("_id");

            var batchItems = await batchItemTable.Find(_ => true).Project<T>(fields).Skip((batchNumber - 1) * splitBy).Limit(batchCount).ToListAsync();

            return batchItems;
        }

        public async Task InsertBatchs(string bulkName, int splitBy, int recordCount, Guid sourceId)
        {
            await this.DropDatabaseAsync(bulkName);

            List<SmsBatchResult> batchs = new List<SmsBatchResult>();

            //List<EmailBatchItemModel> batchItems = new List<EmailBatchItemModel>();

            

            int batchNumber = 1;

            for (int i = 0; i < recordCount; i++)
            {

                //var email = batchItems[i];

                //batchItems.Add(new EmailBatchItemModel { Email = email.Email,Parameters = email.Parameters ,CreatedDate = DateTime.Now,State = SmsState.Unknown });

                if ((i + 1) == (batchNumber * splitBy))
                {
                    SmsBatchResult batchResult = new SmsBatchResult();
                    batchResult.BatchName = bulkName + batchNumber;
                    batchResult.BatchCount = splitBy;
                    batchResult.BatchNumber = batchNumber;
                    batchResult.Status = IFBulkOperationStatus.Ready;
                    batchResult.CreatedDate = DateTime.Now;
                    batchResult.SourceId = sourceId;
                    batchs.Add(batchResult);

                    batchNumber++;
                }

                else if (i == recordCount - 1)
                {

                    SmsBatchResult batchResult = new SmsBatchResult();
                    batchResult.BatchName = bulkName + batchNumber;
                    batchResult.BatchNumber = batchNumber;
                    batchResult.BatchCount = recordCount - ((batchNumber - 1) * splitBy);
                    batchResult.Status = IFBulkOperationStatus.Ready;
                    batchResult.CreatedDate = DateTime.Now;
                    batchResult.SourceId = sourceId;
                    batchs.Add(batchResult);
                }

            }

            //var totalBatchCount = recordCount / splitBy;


          

            var batchTable = this.GetQuery<SmsBatchResult>(nameof(SmsBatchResult), bulkName);

            
            await batchTable.InsertManyAsync(batchs);
            await UpdateBulkBatchCount(bulkName, batchs.Count);
        }

        public async Task InsertOperation<T>(T operation,string operationName) where T : IIFBulkOperation
        {

            var operationTable = this.GetQuery<T>(nameof(IFBulkOperation));
            var filterBuilder = Builders<T>.Filter;
            var batchNameFilter = filterBuilder.Eq(i => i.BulkName, operationName);
            await operationTable.DeleteManyAsync(batchNameFilter);
            operationTable.InsertOne(operation);
        }

        public async Task UpdateBulkBatchCount(string bulkName, int totalBatchCount)
        {
            var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            var options = new UpdateOptions();
            var filter = Builders<IFBulkOperationMongoDb>.Filter.Eq(p => p.BulkName, bulkName);
            var builder = Builders<IFBulkOperationMongoDb>.Update.Set(p => p.BatchCount, totalBatchCount).Set(p => p.UpdatedDate, DateTime.Now);
            var updateResult = await operationTable.UpdateOneAsync(filter, builder, options);
        }

        public async Task UpdateBulkStatus(string bulkName, IFBulkOperationStatus status)
        {
            var options = new UpdateOptions();
            var filter = Builders<IFBulkOperationMongoDb>.Filter.Eq(p => p.BulkName, bulkName);
            var builder = Builders<IFBulkOperationMongoDb>.Update.Set(p => p.Status, status).Set(p => p.UpdatedDate, DateTime.Now);
            var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            var updateResult = await operationTable.UpdateOneAsync(filter, builder, options);
        }

        public async Task<IIFBulkOperation> GetOperation(string bulkName)
        {
            var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            var operation = await operationTable.Find(log => log.BulkName == bulkName).SingleOrDefaultAsync();
            return operation;
        }

        public async Task<List<IFBulkOperation>> GetAllOperations(IFBulkOperationStatus status)
        {
            var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            var fields = Builders<IFBulkOperationMongoDb>.Projection.Exclude("_id");
            var operation = await operationTable.Find(log => log.Status == status).Project<IFBulkOperation>(fields).ToListAsync();
            return operation;
        }

        public async Task<bool> IsBatchExist(string bulkName)
        {
            var smsOperatioModel = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation), this.ApplicationDbName);

            var model = await smsOperatioModel.Find(log => log.BulkName == bulkName).SingleOrDefaultAsync();

            return model != null;
        }

        public async Task UpdateBulkError(string bulkName)
        {
            var options = new UpdateOptions();
            var filter = Builders<IFBulkOperationMongoDb>.Filter.Eq(p => p.BulkName, bulkName);
            var builder = Builders<IFBulkOperationMongoDb>.Update.Set(p => p.Status, IFBulkOperationStatus.Failed).Set(p => p.UpdatedDate, DateTime.Now);
            var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            var updateResult = await operationTable.UpdateOneAsync(filter, builder, options);
        }

        public async Task UpdateBulkCompleted(string bulkName)
        {
            var options = new UpdateOptions();
            var filter = Builders<IFBulkOperationMongoDb>.Filter.Eq(p => p.BulkName, bulkName);
            var builder = Builders<IFBulkOperationMongoDb>.Update.Set(p => p.Status, IFBulkOperationStatus.Completed).Set(p => p.UpdatedDate, DateTime.Now);
            var operationTable = this.GetQuery<IFBulkOperationMongoDb>(nameof(IFBulkOperation));
            var updateResult = await operationTable.UpdateOneAsync(filter, builder, options);
        }
        public async Task<List<SmsBatchResult>> GetResults(string bulkName)
        {
            var fields = Builders<IFBatchResultMongoDb>.Projection.Exclude("_id");
            var resultTable = await this.GetQuery<IFBatchResultMongoDb>(nameof(SmsBatchResult), bulkName).Find(_ => true).Project<SmsBatchResult>(fields).ToListAsync();
            return resultTable;

        }
        public async Task<List<SmsBatchResult>> GetBatchTables(string bulkName)
        {
            var batchTable = this.GetQuery<IFBatchResultMongoDb>(nameof(SmsBatchResult), bulkName);
            var fields = Builders<IFBatchResultMongoDb>.Projection.Exclude("_id");
            var batchs = await batchTable.Find(_ => true).Project<SmsBatchResult>(fields).ToListAsync();

            return batchs;
        }

        public async Task UpdateBatchStatus(string batchName, IFBulkOperationStatus status, string bulkName)
        {
            var options = new UpdateOptions();
            var filter = Builders<IFBatchResultMongoDb>.Filter.Eq(p => p.BatchName, batchName);
            var builder = Builders<IFBatchResultMongoDb>.Update.Set(p => p.Status, status).Set(p => p.UpdateDate, DateTime.Now);
            var batchTable = this.GetQuery<IFBatchResultMongoDb>(nameof(SmsBatchResult), bulkName);
            var updateResult = await batchTable.UpdateOneAsync(filter, builder, options);
        }


        public async Task UpdateBatchCompleted(string bulkName, string batchName, string integrationId)
        {
            var options = new UpdateOptions();
            var filter = Builders<IFBatchResultMongoDb>.Filter.Eq(p => p.BatchName, batchName);
            var builder = Builders<IFBatchResultMongoDb>.Update.Set(p => p.Status, IFBulkOperationStatus.Completed).Set(p => p.UpdateDate, DateTime.Now).Set(p => p.IntegrationId, integrationId);
            var batchTable = this.GetQuery<IFBatchResultMongoDb>(nameof(SmsBatchResult), bulkName);
            var updateResult = await batchTable.UpdateOneAsync(filter, builder, options);
        }

        public async Task UpdateBatchError(string bulkName, string batchName, string ErrorCode)
        {
            var options = new UpdateOptions();
            var filter = Builders<IFBatchResultMongoDb>.Filter.Eq(p => p.BatchName, batchName);
            var builder = Builders<IFBatchResultMongoDb>.Update.Set(p => p.Status, IFBulkOperationStatus.Failed).Set(p => p.UpdateDate, DateTime.Now).Set(p => p.ErrorCode, ErrorCode);
            var batchTable = this.GetQuery<IFBatchResultMongoDb>(nameof(SmsBatchResult), bulkName);
            var updateResult = await batchTable.UpdateOneAsync(filter, builder, options);
        }

        public async Task<long> GetCompletedBatchCount(string bulkName)
        {
            var batchTable = this.GetQuery<SmsBatchResult>(nameof(SmsBatchResult), bulkName);
            var allCompletedCount = await batchTable.CountDocumentsAsync(t => t.Status == IFBulkOperationStatus.Completed);
            return allCompletedCount;
        }

        public async Task<ISmsBatchResult> GetBatchTable(string bulkName, string batchName)
        {

            var batchTable = this.GetQuery<IFBatchResultMongoDb>(nameof(SmsBatchResult), bulkName);

            var batch = await batchTable.Find(b => b.BatchName == batchName).SingleOrDefaultAsync();

            return batch;
        }
    }
}
