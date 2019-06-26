using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.EventBus
{
    public enum EventStateEnum
    {
        
        Publishing = 1,
        Published = 2,
        PublishedFailed = 3,

        
        Processing = 4,
        Processed = 5,
        ProcessFailed = 6
    }
}
