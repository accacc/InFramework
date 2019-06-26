using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Email
{
    public interface  IEmailLogService
    {
        Task LogAsync(string From, string To, string Body, DateTime Date, string Type, bool IsSent, string Subject,Guid UniqueId, Guid SourceId);
    }
}
