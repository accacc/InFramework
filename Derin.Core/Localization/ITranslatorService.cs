using Derin.Core.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFramework.Core.Localization
{
    public interface ITranslatorService
    {
        //string TranslateString(string key);
        //string TranslateStringCached(string key);

        void Translate<LD>(IEnumerable<LD> languageModelList)
            where LD : LanguageDTO;

        //void TranslateConstant<LD>(IEnumerable<LD> languageModelList) where LD : LanguageDTO;


        void TranslateListCurrent<P>(IEnumerable<P> languageModelList)
            where P : LanguageDTO;


        L GetObjectCurrentLanguageCache<L>(int objectId) where L : class, ILanguageEntity;

        CultureInfo CurrentCulture { get; }
        CultureInfo DefaultCulture { get; set; }
        bool IsDefaultLanguage();


    }
}
