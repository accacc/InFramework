using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Persistence.EF.Audit
{
    public class EntityElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }

            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("audit", IsRequired = false)]
        public string Audit
        {
            get
            {
                return (string)this["audit"];
            }

            set
            {
                this["audit"] = value;
            }
        }
    }
}
