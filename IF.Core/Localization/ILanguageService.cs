using IF.Persistence.EF.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Localization
{
    public interface ILanguageService
    {

        IEnumerable<T> GetLanguageObjectList<T>() where T : class, ILanguageableEntity;

        List<T> GetLanguageObject<T>(object Id) where T : class, ILanguageEntity;

        void UpdateLanguages<L>(List<L> list, object Id) where L : ILanguageEntity;

        void UpdateLanguages<L>(LanguageFormModel model) where L : ILanguageEntity;

        
    }
}
