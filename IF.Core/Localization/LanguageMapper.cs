using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.Core.Localization
{
    public abstract class LanguageMapper
    {
        private  List<LanguageMap> LanguageMaps = new List<LanguageMap>();

        public LanguageMapper()
        {
            LanguageMaps.Clear();
        }

        public LanguageMap GetMapByDto<T>() where T : ILanguageData
        {
            return this.LanguageMaps.SingleOrDefault(l => l.Dto == typeof(T));
            
        }

        public LanguageMap GetMapByDto(Type type)
        {
            return this.LanguageMaps.SingleOrDefault(l => l.Dto == type);

        }

        public LanguageMap GetMapByEntity<T>() where T : IEntity
        {
            return this.LanguageMaps.SingleOrDefault(l => l.Entity == typeof(T));
        }

        public LanguageMap GetMapByLanguageEntity(Type type)
        {
            return this.LanguageMaps.SingleOrDefault(l => l.Language == type);
        }

        public void AddMap<D, L,E>() where D : ILanguageData where L : ILanguageEntity where E : IEntity
        {
            LanguageMaps.Add(new LanguageMap { Dto = typeof(D), Language = typeof(L) , Entity = typeof(E) });
        }
    }

    public class LanguageMap
    {
        public Type Entity { get; set; }

        public Type Language { get; set; }

        public Type Dto { get; set; }

        //public IMappingExpression mapper { get; set; }
    }
}
