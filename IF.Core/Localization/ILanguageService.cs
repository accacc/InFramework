using IF.Persistence.EF.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Localization
{
    public interface ILanguageService
    {


        LanguageFormModel GetLanguageFormModel(Type entityType, object Id);

        List<Type> GetAllLanguageEntities(Assembly[] assemblies);

        LanguageGridModel GetLanguageGridModel(string LanguageObject);

        IEnumerable<T> GetLanguageObjectList<T>() where T : class, ILanguageableEntity;

        T GetLanguageObject<T>(object Id) where T : class, ILanguageEntity;
        void UpdateLanguages(Type entityType, LanguageFormModel model);

        CultureInfo[] Cultures { get; }
        CultureInfo DefaultCulture { get; }

        CultureInfo CurrentCulture { get; }
        bool IsDefaultLanguage();
    }
}
