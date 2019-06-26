//using IF.Core.Cache;
//using IF.Core.Localization;
//using Derin.Localization;
//using Derin.Persistence.EntityFramework;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;

//namespace InFramework.Core.Persistence.EntityFramework.Localization
//{
//    public class LanguageService : GenericRepository, ILanguageService
//    {

//        ICacheService cacheService;

//        public LanguageService(DbContext context, ICacheService cacheService)
//            : base(context)
//        {
//            this.cacheService = cacheService;
//        }


//        public IEnumerable<L> GetLanguageObjectList<L>() where L : class, ILanguageableEntity
//        {
//            return this.GetQuery<L>().ToList();
//        }

//        public List<T> GetLanguageObject<T>(int Id) where T : class, ILanguageEntity
//        {
//            string tableName = this.DbContext.GetTableName<T>();

//            string sql = "select * from " + tableName + " where ObjectId = {0}";

//            return this.DbContext.Database.SqlQuery<T>(sql, Id).ToList();

//        }

//        public void UpdateLanguages<L>(List<L> list,int Id) where L : ILanguageEntity
//        {
            
//        }
//    }
//}
