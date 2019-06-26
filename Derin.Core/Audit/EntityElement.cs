using System.Configuration;

namespace Derin.Core.Audit
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
