using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Sms
{

    public interface ISmsLogService
    {
        Task LogAsync(string number, string message,DateTime Date, bool IsSent, string Error,string IntegrationId,Guid UniqueId, Guid SourceId);
    }
}

