using IF.Core.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Localization
{
    public abstract class LanguageMapper
    {
        internal List<LanguageMap> LanguageMaps = new List<LanguageMap>();

        public LanguageMapper()
        {
            LanguageMaps.Clear();
        }

        public void AddMap<D, L>() where D : LanguageDto where L : ILanguageEntity
        {
            LanguageMaps.Add(new LanguageMap { Dto = typeof(D), Language = typeof(L) });
        }
    }
}
