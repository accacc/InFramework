using IF.Core.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace IF.Core.Localization
{
    public interface ITranslatorService
    {
        //string TranslateString(string key);
        //string TranslateStringCached(string key);

        void Translate<LD>(IEnumerable<LD> languageModelList)
            where LD : LanguageDto;

        //void TranslateConstant<LD>(IEnumerable<LD> languageModelList) where LD : LanguageDTO;


        void TranslateListCurrent<P>(IEnumerable<P> languageModelList)
            where P : LanguageDto;


        L GetObjectCurrentLanguageCache<L>(int objectId) where L : class, ILanguageEntity;

        CultureInfo CurrentCulture { get; }
        CultureInfo DefaultCulture { get; set; }
        bool IsDefaultLanguage();


    }
}
