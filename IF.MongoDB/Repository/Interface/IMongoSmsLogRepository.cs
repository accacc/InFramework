using IF.Core.Data;
using IF.Core.Sms;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Interface
{
    public interface IMongoSmsLogRepository: IMongoDbGenericRepository
    {


        Task<PagedListResponse<ISmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int PageSize = 0, int PageNumber = 50);

        Task<PagedListResponse<IFBulkOperation>> GetPaginatedSmsBulkOneToManyOperationAsync(DateTime BeginDate, DateTime EndDate, string bulkName, int PageNumber = 0, int PageSize = 50);

        Task<List<SmsBatchResult>> GetSmsBulkResultOneToManyList(string bulkName);
        Task<List<SmsBatchResult>> GetSmsBulkResultManyToManyList(string bulkName);
        Task<PagedListResponse<IFBulkOperation>> GetPaginatedSmsBulkManyToManyOperationAsync(DateTime beginDate, DateTime endDate, string bulkName, int pageNumber, int pageSize);
        Task<PagedListResponse<SmsBatchItemModel>> GetPaginatedSmsList(DateTime BeginDate, DateTime EndDate, string bulkName, string number, BatchItemState? smsState, int pageNumber, int pageSize);
    }
}
