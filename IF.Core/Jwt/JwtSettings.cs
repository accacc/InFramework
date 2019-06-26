using IF.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Jwt
{

    public class JwtSettings : IIFSettings
    {
        public string SchemeName { get; set; }

        public string SecretKey { get; set; }

        public int ExpireMinutes { get; set; }
    }
}
