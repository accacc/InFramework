using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{

    public enum SmsState
    {
        Success = 3,
        Failed = 5,
        Waiting = 6,
        Expired = 9,
        Unknown = 1

    }
    public class IFSmsNumberResult
    {
        public string Number { get; set; }
        public SmsState State { get; set; }

        public DateTime SentDate { get; set; }
    }
}
