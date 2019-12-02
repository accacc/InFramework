using IF.Core.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace IF.Core.Localization
{
    public interface ITranslatorService
    {
        void Translate<LD>(IEnumerable<LD> languageModelList) where LD : class;      
        L GetObjectCurrentLanguageCache<L>(int objectId) where L : class, ILanguageEntity;
    }
}
