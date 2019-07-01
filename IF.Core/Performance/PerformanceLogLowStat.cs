using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Performance
{
    public class PerformanceLogLowStat
    {
        public string MethodName { get; set; }
        public double Maximimum { get; set; }
        public double Minimum { get; set; }

        public int Count { get; set; }
        public double Avarage { get; set; }
    }
}
