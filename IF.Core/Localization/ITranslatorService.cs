using IF.Core.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace IF.Core.Localization
{
    public interface ITranslatorService
    {
        void Translate<LD>(IEnumerable<LD> languageModelList) where LD : ILanguageData;
        
    }
}
