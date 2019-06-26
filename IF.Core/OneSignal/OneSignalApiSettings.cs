using IF.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.OneSignal
{
    public class OneSignalApiSettings:IIFSettings
    {
        public string ApplicationId { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}
