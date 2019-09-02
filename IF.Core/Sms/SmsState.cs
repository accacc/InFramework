using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public enum BatchItemState
    {
        Success = 3,
        Failed = 5,
        Waiting = 6,
        Expired = 9,
        Unknown = 1

    }
}
