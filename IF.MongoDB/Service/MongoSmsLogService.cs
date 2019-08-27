using IF.Core.Data;
using IF.Core.Sms;
using IF.MongoDB.Model;
using IF.MongoDB.Repository;
using IF.MongoDB.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Service
{

    public class MongoSmsLogService : ISmsLogService
    {

        private readonly IMongoSmsLogRepository smsLogRepository;

        public MongoSmsLogService(IMongoSmsLogRepository smsLogRepository)
        {
            this.smsLogRepository = smsLogRepository;
        }

        public async Task<PagedListResponse<ISmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int PageSize = 0, int PageNumber = 50)
        {
            return await this.smsLogRepository.GetPaginatedAsync(BeginDate, EndDate,  number, PageNumber,PageSize);
        }

        public async Task<PagedListResponse<SmsBulkManyToManyOperation>> GetPaginatedSmsBulkManyToManyOperationAsync(DateTime beginDate, DateTime endDate, string bulkName, int pageNumber, int pageSize)
        {
            return await this.smsLogRepository.GetPaginatedSmsBulkManyToManyOperationAsync(beginDate, endDate, bulkName, pageNumber, pageSize);
        }

        public async Task<PagedListResponse<SmsBulkOneToManyOperation>> GetPaginatedSmsBulkOneToManyOperationAsync(DateTime BeginDate, DateTime EndDate, string bulkName, int PageNumber = 0, int PageSize = 50)
        {
            return await this.smsLogRepository.GetPaginatedSmsBulkOneToManyOperationAsync(BeginDate, EndDate, bulkName, PageNumber,PageSize);
        }

        public async Task<List<SmsBatchResult>> GetSmsBulkResultManyToManyList(string bulkName)
        {
            return await this.smsLogRepository.GetSmsBulkResultManyToManyList(bulkName);
        }

        public async Task<List<SmsBatchResult>> GetSmsBulkResultOneToManyList(string bulkName)
        {
            return await this.smsLogRepository.GetSmsBulkResultOneToManyList(bulkName);
        }

        public async Task LogAsync(string number, string message, DateTime Date, bool IsSent,string Error, string IntegrationId, Guid UniqueId,Guid SourceId)
        {
            SmsLog smsLog = new SmsLog();

            smsLog.Number = number;
            smsLog.Message = message;
            smsLog.Date = Date;
            smsLog.IsSent = IsSent;
            smsLog.UniqueId = UniqueId;
            smsLog.Error = Error;
            smsLog.IntegrationId = IntegrationId;
            smsLog.SourceId = SourceId;

            await this.smsLogRepository.AddAsync(smsLog);
        }
    }
}
