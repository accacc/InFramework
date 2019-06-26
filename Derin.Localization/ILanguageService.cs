using IF.Core.Localization;
using IF.Core.Persistence;
using System.Collections.Generic;

namespace Derin.Localization
{
    public interface ILanguageService : IRepository
    {

        IEnumerable<T> GetLanguageObjectList<T>() where T : class, ILanguageableEntity;

        List<T> GetLanguageObject<T>(int Id) where T : class, ILanguageEntity;

        void UpdateLanguages<L>(List<L> list,int Id) where L : ILanguageEntity;
    }
}
