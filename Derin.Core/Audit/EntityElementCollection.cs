using System.Configuration;

namespace Derin.Core.Audit
{
    public class EntityElementCollection : ConfigurationElementCollection
    {
        public EntityElement this[int index]
        {
            get
            {
                return (EntityElement)BaseGet(index);
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EntityElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EntityElement)element).Name;
        }
    }
}
