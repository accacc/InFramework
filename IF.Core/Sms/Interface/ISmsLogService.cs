using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Sms
{

    public interface ISmsLogService
    {
        Task LogAsync(string number, string message, DateTime Date, bool IsSent, string Error, string IntegrationId, Guid UniqueId, Guid SourceId);

        Task<PagedListResponse<ISmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int PageSize = 0, int PageNumber = 50);

        Task<PagedListResponse<SmsBulkOneToManyOperation>> GetPaginatedSmsBulkOneToManyOperationAsync(DateTime BeginDate, DateTime EndDate, string bulkName, int PageNumber = 0, int PageSize = 50);

        Task<PagedListResponse<SmsModel>> GetPaginatedSmsListAsync(DateTime BeginDate, DateTime EndDate, string bulkName, string Number, SmsState? smsState, int PageNumber = 0, int PageSize = 50);

        Task<List<SmsBatchResult>> GetSmsBulkResultOneToManyList(string bulkName);
        Task<PagedListResponse<SmsBulkManyToManyOperation>> GetPaginatedSmsBulkManyToManyOperationAsync(DateTime beginDate, DateTime endDate, string bulkName, int pageNumber, int pageSize);
        Task<List<SmsBatchResult>> GetSmsBulkResultManyToManyList(string bulkName);
    }
}

