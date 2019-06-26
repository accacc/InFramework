using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Derin.Persistence.EF
{
    public static class DbContextExtension
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return GetTableName<T>(objectContext);
        }

        public static string GetTableName(this DbContext context, DbEntityEntry entity)
        {

            Type entityType = entity.Entity.GetType();

            if (entityType.Namespace == "System.Data.Entity.DynamicProxies")
            {
                entityType = entityType.BaseType;
            }
            return entityType.Name;

            //ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            //return GetTableName(objectContext, type);
        }

        public static EntityKeyMember[] GetPrimaryKeys(this DbContext context, DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues;
        }

        public static object GetPrimaryKeyValue(this DbContext context, DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        private static string GetTableName<T>(ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;
            return table;   
        }

        public static T CreateEntity<T>(this DbContext context, Type entityType) where T : class
        {
            DbSet set = context.Set(entityType);
            T entity = set.Create() as T;
            set.Add(entity);
            return entity;
        }
    }
}
