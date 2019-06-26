using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.SendGrid
{
    public class SendGridEmailSettings
    {
        public string FromMailAddress { get; set; }

        public string ApiKey { get; set; }
    }
}
