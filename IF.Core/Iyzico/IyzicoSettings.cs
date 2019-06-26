using IF.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Iyzico
{
    public class IyzicoSettings: IIFSettings
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string BaseUrl { get; set; }
    }
}
