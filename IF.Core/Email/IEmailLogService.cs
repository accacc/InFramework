using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Email
{
    public interface  IEmailLogService
    {
        Task LogAsync(string From, string To, string Body, DateTime Date, string Type, bool IsSent, string Subject,Guid UniqueId, Guid SourceId);



        Task<PagedListResponse<IEmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int PageNumber = 0, int PageSize = 50);

        Task<string> GetBodyAsync(Guid id);
    }
}
