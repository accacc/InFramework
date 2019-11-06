using IF.Core.Data;
using IF.Core.EventBus;
using IF.Core.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Batch
{
    public abstract class IFBatchProcessBase
    {
        private readonly IBatchRepositoryBase repository;
        private readonly IEventBus eventBus;

        public IFBatchProcessBase(IBatchRepositoryBase repository, IEventBus eventBus)
        {
            this.repository = repository;
            this.eventBus = eventBus;
        }


        public abstract Task<BatchResponse> SendBatchItemAsync(IIFBulkOperation operation, ISmsBatchResult batchResult, int BatchNumber);


        public async Task UpdateBatchResult(string BulkName, string BatchName, int TotalBatch, bool IsSuccess, string ErrorCode = null, string IntegrationId = null)
        {


            if (!IsSuccess)
            {
                await this.repository.UpdateBatchError(BulkName, BatchName, ErrorCode);
                await this.repository.UpdateBulkError(BulkName);
            }
            else
            {
                await this.repository.UpdateBatchCompleted(BulkName, BatchName, IntegrationId);

                long allCompletedCount = await this.repository.GetCompletedBatchCount(BulkName);

                if (TotalBatch == allCompletedCount)
                {
                    await this.repository.UpdateBulkCompleted(BulkName);
                }

            }
        }

        public async Task<BaseResponse> SendBulk<T>(IFBatchEvent @event) where T : IFBatchEvent
        {

            BaseResponse response = new BaseResponse();
            response.IsSuccess = true;

            IIFBulkOperation operation = await this.repository.GetOperation(@event.BulkName);

            if (operation == null)
            {
                throw new Exception(@event.BulkName + " : Boyle bir bulk bulunamadi");
            }


            if (operation.Status != IFBulkOperationStatus.Ready)
            {
                throw new Exception(operation.BulkName + " : Bu işlem daha önce çalıştırılmış");
            }

            List<SmsBatchResult> resultTable = await this.repository.GetResults(operation.BulkName);

            int batchTotal = resultTable.Sum(s => s.BatchCount);



            if (batchTotal != operation.Total)
            {
                throw new Exception(operation.BulkName + " : Batch içindeki kayıt sayısı değişmiş, lütfen kontrol ediniz...");
            }

            if (operation.SplitBy > operation.Total)
            {
                throw new Exception(operation.BulkName + " : SplitBy toplam kayıt sayısından büyük olamaz, lütfen kontrol ediniz...");
            }

            await this.repository.UpdateBulkStatus(operation.BulkName, IFBulkOperationStatus.InProgress);

            foreach (var smsBatchResult in resultTable)
            {
                @event.BulkName = operation.BulkName;
                @event.BatchName = smsBatchResult.BatchName;
                @event.BatchNumber = smsBatchResult.BatchNumber;
                @event.BatchCount = smsBatchResult.BatchCount;

                this.eventBus.Publish(@event);
            }


            return response;
        }

        public async Task SendBatchItem(string BulkName, string BatchName, int BatchCount, int BatchNumber)
        {


            IIFBulkOperation operation = await this.repository.GetOperation(BulkName);

            if (operation == null)
            {
                throw new System.Exception("BatchName " + BulkName + " bulunamadı");
            }




            ISmsBatchResult batch = await this.repository.GetBatchTable(BulkName, BatchName);

            if (batch == null)
            {
                throw new Exception("Batch bulunamadi");
            }

            if (batch.BatchCount != BatchCount)
            {
                throw new Exception("Hatali Batch");
            }

            await this.repository.UpdateBatchStatus(BatchName, IFBulkOperationStatus.InProgress, BulkName);

            var response = await this.SendBatchItemAsync(operation, batch, BatchNumber);

            await this.UpdateBatchResult(BulkName, BatchName, operation.BatchCount, response.IsSuccess, response.ErrorCode, response.IntegrationId);




        }
        protected async Task<BaseResponse> InitBulkAsync(string BulkName, int SplitBy, int batchCount, bool Force, string operationTableName, string type, Guid SourceId)
        {
            BaseResponse response = new BaseResponse();

            response.IsSuccess = true;

            if (SplitBy <= 0) { throw new Exception("SplitBy sifirdan küçük olamaz"); }

            if (String.IsNullOrWhiteSpace(BulkName)) { throw new Exception("BulkName boş olamaz"); }

            if (!Force)
            {

                bool IsExist = await this.repository.IsBatchExist(BulkName);

                if (IsExist)
                {
                    throw new Exception(BulkName + " bu paket zaten var.Eğer tamamen değiştirmek istiyorsaniz 'Force' parametresini kullanin");
                }
            }

            if (SplitBy > batchCount)
            {
                throw new Exception(BulkName + " : SplitBy toplam kayıt sayısından büyük olamaz, lütfen kontrol ediniz...");
            }


            await this.repository.InsertOperation(BulkName, SplitBy, batchCount, BulkName, type);


            await this.repository.InsertBatchs(BulkName, SplitBy, batchCount, SourceId);



            return response;
        }
    }
}
