using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;

namespace IF.Persistence.EF.Audit
{

    public class ShadowAuditing : IAuditCommand
    {

        private static Dictionary<Type, AuditTypeInfo> auditTypes = new Dictionary<Type, AuditTypeInfo>();

        List<IShadowAuditableEntity> auditableEntities = new List<IShadowAuditableEntity>();

        List<TempShadowClass> tempShadows = new List<TempShadowClass>();




        public static bool AuditEnabled { get; private set; }
        public static string CreatedColumnName = "Created";
        public static string CreatedByColumnName = "CreatedBy";
        public static string LogTypeColumnName = "LogType";
        public static string ModifiedColumnName = "Modified";
        public static string ModifiedByColumnName = "ModifiedBy";
        public static string LogDateColumnName = "LogDate";
        public static string ObjectIdColumnName = "ObjectId";
        public static string UniqueIdColumnName = "UniqueId";
        public static string ChannelColumnName = "Channel";



        public static void RegisterAuditType<E, A, P>(Expression<Func<E, P>> primaryKeyName) where E : IShadowAuditableEntity where A : IShadowAuditEntity
        {


            Type auditableEntityType = typeof(E);
            Type auditEntityType = typeof(A);



            if (auditTypes.ContainsKey(auditableEntityType))
            {
                throw new ArgumentException("Type already registered for auditing.", "auditableEntityType");
            }

            var iface = auditableEntityType.GetInterface("IShadowAuditableEntity");


            if (iface == null)
            {
                throw new ArgumentException("Entity does implement IShadowAuditableEntity", "auditableEntityType");
            }



            if (auditEntityType != null)
            {
                AuditTypeInfo info = new AuditTypeInfo { AuditableEntityType = auditableEntityType, AuditEntityType = auditEntityType };


                MemberExpression propertyBody = primaryKeyName.Body as MemberExpression;

                info.PrimaryKeyName = propertyBody.Member.Name;

                var properties = auditEntityType.GetProperties();

                var entityProperties = auditableEntityType.GetProperties().ToDictionary(x => x.Name);

                foreach (var property in properties)
                {
                    if (entityProperties.ContainsKey(property.Name))
                    {
                        if (property.PropertyType == entityProperties[property.Name].PropertyType)
                        {
                            info.AuditProperties.Add(property.Name);
                        }
                    }
                }

                auditTypes.Add(auditableEntityType, info);
            }


        }

        public void BeforeSave(AuditContext context)
        {
            foreach (EntityEntry<IShadowAuditableEntity> auditable in context.DbContext.ChangeTracker.Entries<IShadowAuditableEntity>())
            {
                if (auditable.State == EntityState.Added
                    || auditable.State == EntityState.Modified
                    || auditable.State == EntityState.Deleted)
                {

                    Type entityType = auditable.Entity.GetType();

                    if (entityType.Namespace.Contains("Entity.DynamicProxies"))
                    {
                        entityType = entityType.BaseType;
                    }

                    if (auditTypes.ContainsKey(entityType) && auditTypes[entityType].AuditEntityType != null)
                    {
                        var tempShadowClass = this.CreateTempShadowClass(auditable, auditTypes[entityType], context.DbContext);

                        this.tempShadows.Add(tempShadowClass);

                        auditableEntities.Add(auditable.Entity);
                    }
                }

            }
        }



        public void AfterSave(AuditContext context)
        {
            foreach (var temp in tempShadows)
            {

                Type auditEntityType = temp.AuditTypeInfo.AuditEntityType;

                if (auditEntityType.Namespace.Contains("Entity.DynamicProxies"))
                {
                    auditEntityType = auditEntityType.BaseType;
                }


                var auditableEntity = this.auditableEntities.Where(c => c.UniqueId == temp.UniqueId).SingleOrDefault();


                string ObjectId = temp.AuditTypeInfo.AuditableEntityType.GetProperty(temp.AuditTypeInfo.PrimaryKeyName).GetValue(auditableEntity, null).ToString();

                temp.ObjectId = ObjectId;

                var entity = this.CreateAuditEntity(temp, temp.AuditTypeInfo, context);

                context.DbContext.Add(entity);

            }
        }

        private TempShadowClass CreateTempShadowClass(EntityEntry<IShadowAuditableEntity> entityEntry, AuditTypeInfo auditTypeInfo, DbContext context)
        {
            TempShadowClass shadowClass = new TempShadowClass();

            shadowClass.AuditTypeInfo = auditTypeInfo;
            shadowClass.State = entityEntry.State;
            shadowClass.UniqueId = entityEntry.Entity.UniqueId;

            foreach (string propertyName in auditTypeInfo.AuditProperties)
            {

                if (entityEntry.State == EntityState.Added)
                {
                    shadowClass.Properties.Add(new TempShadowClassProperty
                    {
                        Value = entityEntry.Property(propertyName).CurrentValue,
                        PropertyName = propertyName,
                    });


                }
                else
                {
                    shadowClass.Properties.Add(new TempShadowClassProperty
                    {
                        Value = entityEntry.Property(propertyName).OriginalValue,
                        PropertyName = propertyName,
                    });
                }
            }

            var UpdateDateTime = DateTime.Now; //context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();


            ClaimsPrincipal principal = Thread.CurrentPrincipal as ClaimsPrincipal;



            string identityName = "";

            if (principal != null && principal.Identity != null)
            {
                identityName = principal.Identity.Name;
            }

            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = CreatedColumnName, Value = UpdateDateTime });
            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = CreatedByColumnName, Value = identityName });
            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = ModifiedColumnName, Value = UpdateDateTime });
            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = ModifiedByColumnName, Value = identityName });
            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = LogDateColumnName, Value = UpdateDateTime });
            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = UniqueIdColumnName, Value = entityEntry.Entity.UniqueId });





            int logType = 0;

            switch (entityEntry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Added:
                    logType = 4;
                    break;
                case EntityState.Deleted:
                    logType = 8;
                    break;
                case EntityState.Modified:
                    logType = 16;
                    break;
                default:
                    break;
            }


            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = LogTypeColumnName, Value = logType });


            if (principal != null)
            {

                var channelId = principal.Claims.Where(c => c.Type == ChannelColumnName).SingleOrDefault();

                if (channelId != null)
                {
                    shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = ChannelColumnName, Value = channelId.Value });

                }

            }

            return shadowClass;

        }

        private IAuditEntity CreateAuditEntity(TempShadowClass temp, AuditTypeInfo auditTypeInfo, AuditContext context)
        {



            IShadowAuditEntity auditEntity = Activator.CreateInstance(auditTypeInfo.AuditEntityType) as IShadowAuditEntity;

            EntityEntry auditEntityEntry = context.DbContext.Entry(auditEntity);


            foreach (string propertyName in auditTypeInfo.AuditProperties)
            {
                if (auditTypeInfo.PrimaryKeyName == propertyName) continue;

                auditEntityEntry.Property(propertyName).CurrentValue = temp.Properties.Single(p => p.PropertyName == propertyName).Value;
            }


            auditEntityEntry.Property(CreatedColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == CreatedColumnName).Value;
            auditEntityEntry.Property(CreatedByColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == CreatedByColumnName).Value;
            auditEntityEntry.Property(ModifiedColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == ModifiedColumnName).Value;
            auditEntityEntry.Property(ModifiedByColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == ModifiedByColumnName).Value;
            auditEntityEntry.Property(LogDateColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == LogDateColumnName).Value;
            auditEntityEntry.Property(LogTypeColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == LogTypeColumnName).Value;

            if (temp.Properties.Any(p => p.PropertyName == ChannelColumnName))
            {
                auditEntityEntry.Property(ChannelColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == ChannelColumnName).Value;

            }

            auditEntityEntry.Property(ObjectIdColumnName).CurrentValue = temp.ObjectId;

            return auditEntity;
        }


    }


  

   
}
