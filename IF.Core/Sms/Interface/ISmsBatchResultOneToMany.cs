using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public interface ISmsBatchResultOneToMany
    {

        int BatchNumber { get; set; }
        string BatchName { get; set; }

        SmsOperationStatus Status { get; set; }

        string ErrorCode { get; set; }


        DateTime CreatedDate { get; set; }

        Guid SourceId { get; set; }

        string IntegrationId { get; set; }

        int BatchCount { get; set; }


        DateTime UpdateDate { get; set; }
    }
}
