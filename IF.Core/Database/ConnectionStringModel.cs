using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Database
{
    public class ConnectionStringModel
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public int MinPoolSize { get; set; }
        public int MaxPoolSize { get; set; }
        public int LoadBalanceTimeout { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }
    }




}
