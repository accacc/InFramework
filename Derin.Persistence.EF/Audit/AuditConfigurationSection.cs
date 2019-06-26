using Derin.Persistence.EF.Audit;
using IF.Core.Audit;
using System.Configuration;

namespace Derin.Persistence.EF
{
    internal class AuditConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("entities", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EntityElementCollection), AddItemName = "add")]
        internal EntityElementCollection Entities
        {
            get
            {
                return (EntityElementCollection)base["entities"];
            }
        }

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
        internal bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }

            set
            {
                this["enabled"] = value;
            }
        }
    }
}
