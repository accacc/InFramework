using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Sms.Turatel
{
    public class SmsLimitResult
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Desc { get; set; }
        public decimal? Data { get; set; }
    }
}
