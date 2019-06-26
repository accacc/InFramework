﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Performance
{
    public interface IPerformanceLog
    {
        Guid UniqueId { get; set; }

        string MethodName { get; set; }

        double  ExecutionTime { get; set; }
        DateTime  ExecutionDate { get; set; }




    }
}
