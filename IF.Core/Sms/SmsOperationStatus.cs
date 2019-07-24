using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public enum SmsOperationStatus
    {
        Ready = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3,
        Failed = 4

    }
}
