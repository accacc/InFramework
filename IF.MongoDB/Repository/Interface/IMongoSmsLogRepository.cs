using IF.Core.Data;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{
    public interface IMongoSmsLogRepository: IRepository
    {
        Task<IEnumerable<SmsLog>> GetLogsAsync(string bodyText, DateTime updatedFrom, long headerSizeLimit);


        Task<PagedListResponse<SmsLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string number, int skipNumber = 0, int takeNumber = 50);
    }
}
