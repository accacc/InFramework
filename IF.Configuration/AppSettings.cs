using IF.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Configuration
{
    public class AppSettingsCore : IAppSettingsCore
    {

        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string ApplicationCode { get; set; }



    }
}
