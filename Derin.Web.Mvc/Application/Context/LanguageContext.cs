using IF.Core.Localization;
using System.Collections.Generic;

namespace Derin.Core.Mvc.Application.Context
{
    public class LanguageContext
    {


        public IEnumerable<SystemLanguageDto> Languages()
        {
            List<SystemLanguageDto> languages = new List<SystemLanguageDto>();

            languages.Add(new SystemLanguageDto { Id = 1033, Name = "English", Code = "en-US" });

            return languages;
        }


        public LanguageContext()
        {

        }

    }
}
