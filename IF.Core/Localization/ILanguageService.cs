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

        List<T> GetLanguageObject<T>(int Id) where T : class, ILanguageEntity;

        void UpdateLanguages<L>(List<L> list, int Id) where L : ILanguageEntity;
    }
}
