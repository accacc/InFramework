using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Sms.Turatel
{
    public class SmsResult
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Desc { get; set; }
        public string IntegrationId { get; set; }
        public string EncryptedCode { get; set; }
    }
}
