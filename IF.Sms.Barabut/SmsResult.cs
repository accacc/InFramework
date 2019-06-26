using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Sms.Barabut
{
    public class SmsResult
    {
        public int Code
        {
            get;
            set;
        }

        public string Desc
        {
            get;
            set;
        }

        public bool IsSuccess
        {
            get;
            set;
        }

        public string IntegrationId { get; set; }

        public SmsResult()
        {
        }
    }
}
