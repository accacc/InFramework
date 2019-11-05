using IF.Core.Sms;
using IF.Core.Sms.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.Batch
{
    public interface IBatchRepositoryBase
    {


        Task InsertOperation(string BulkName, int SplitBy, int Total, string operationName,string type);

        Task InsertOperation<T>(T operation, string operationName) where T : IIFBulkOperation;

        Task<List<T>> GetBatchItemList<T>(string bulkName, string batchItemName, int batchNumber, int splitBy, int batchCount) where T : IFBatchItemModel;

        Task<bool> IsBatchExist(string bulkName);

        Task InsertBatchs(string bulkName,int splitBy, int recordCount, Guid sourceId);

        Task<List<SmsBatchResult>> GetResults(string bulkName);

        Task<IIFBulkOperation> GetOperation(string bulkName);

        Task UpdateBulkStatus(string bulkName, IFBulkOperationStatus status);

        Task<ISmsBatchResult> GetBatchTable(string bulkName, string batchName);

        Task UpdateBatchStatus(string batchName, IFBulkOperationStatus status, string bulkName);



        Task UpdateBatchError(string bulkName, string batchName, string ErrorCode);

        Task UpdateBulkError(string bulkName);

        Task UpdateBatchCompleted(string bulkName, string batchName, string integrationId);

        Task<long> GetCompletedBatchCount(string bulkName);

        Task UpdateBulkCompleted(string bulkName);

        Task<List<IFBulkOperation>> GetAllOperations(IFBulkOperationStatus status);

        

        Task<List<SmsBatchResult>> GetBatchTables(string bulkName);
    }
}
