using System;

namespace Derin.Core.Enum
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class LocalizationEnumAttribute : Attribute
    {
        public Type ResourceClassType
        {
            get;
            protected set;
        }

        public LocalizationEnumAttribute(Type resourceClassType)
        {
            this.ResourceClassType = resourceClassType;
        }
    }
}
