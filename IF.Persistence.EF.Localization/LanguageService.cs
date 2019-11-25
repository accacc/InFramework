using IF.Core.Cache;
using IF.Core.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.Persistence.EF.Localization
{
    public class LanguageService : GenericRepository, ILanguageService
    {

        ICacheService cacheService;

        public LanguageService(DbContext context, ICacheService cacheService)
            : base(context)
        {
            this.cacheService = cacheService;
        }


        public IEnumerable<L> GetLanguageObjectList<L>() where L : class, ILanguageableEntity
        {
            return this.GetQuery<L>().ToList();
        }

        public List<T> GetLanguageObject<T>(int Id) where T : class, ILanguageEntity
        {
            //string tableName = this.DbContext.GetTableName<T>();

            //string sql = "select * from " + tableName + " where ObjectId = {0}";

            //return this.DbContext.Database.SqlQuery<T>(sql, Id).ToList();

            return null;

        }


        public string GetTableName<T>() where T : class
        {


            var model = this.DbContext.Model;
            var entityTypes = model.GetEntityTypes();
            var entityType = entityTypes.First(t => t.ClrType == typeof(T));
            var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
            var tableName = tableNameAnnotation.Value.ToString();
            return tableName;
        }

        public void UpdateLanguages<L>(List<L> list, int Id) where L : ILanguageEntity
        {

        }
    }
}
