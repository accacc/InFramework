using IF.Core.Cache;
using IF.Core.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
    

namespace IF.Persistence.EF.Localization
{
    public class LanguageService : ILanguageService
    {

        private readonly ICacheService cacheService;
        private readonly IRepository repository;

        public LanguageService(IRepository repository, ICacheService cacheService)
            
        {
            this.cacheService = cacheService;
            this.repository = repository;
        }


        public IEnumerable<L> GetLanguageObjectList<L>() where L : class, ILanguageableEntity
        {
            return this.repository.GetQuery<L>().ToList();
        }

        public List<T> GetLanguageObject<T>(object Id) where T : class, ILanguageEntity
        {
            string primaryKeys = this.repository.GetPrimarykeyName(typeof(T));

            return this.repository.GetQuery<T>().Where($"{primaryKeys} == @0", Id).ToList();

            

        }


        //public string GetTableName<T>() where T : class
        //{

            
        //    var model = this.DbContext.Model;
        //    var entityTypes = model.GetEntityTypes();
        //    var entityType = entityTypes.First(t => t.ClrType == typeof(T));
        //    var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
        //    var tableName = tableNameAnnotation.Value.ToString();
        //    return tableName;
        //}

        public void UpdateLanguages<L>(List<L> list, object Id) where L : ILanguageEntity
        {

        }

        public void UpdateLanguages<L>(LanguageFormModel model) where L : ILanguageEntity
        {
            foreach (var language in model.Languages)
            {
                
            }
        }
    }
}
