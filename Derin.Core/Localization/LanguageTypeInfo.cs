using System;
using System.Collections.ObjectModel;

namespace Derin.Core.Localization
{
    public class LanguageTypeInfo
    {
        public LanguageTypeInfo()
        {
            this.Properties = new Collection<string>();
        }

        public Type LanguageableType { get; set; }
        public Type LanguageType { get; set; }
        public Collection<string> Properties { get; set; }
    }
}
