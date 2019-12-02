using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace IF.Core.Localization
{
    public class LocalizationSettings
    {
        public int DefaultLanguage { get; set; }
        public CultureInfo[] Cultures { get; set; }       

        public Assembly[] Assemblies { get; set; }

        public LocalizationSettings(CultureInfo[] Cultures, int DefaultLanguage, Assembly[] Assemblies)
        {
            this.Cultures = Cultures;
            this.DefaultLanguage = DefaultLanguage;
            this.Assemblies = Assemblies;
        }
        
    }
}
