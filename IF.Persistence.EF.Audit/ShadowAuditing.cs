//using System;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Derin.Persistence.EF.Audit;
//using IF.Core.Audit;
//using System.Linq.Expressions;
//using System.Threading;
//using System.Security.Claims;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

//namespace IF.Persistence.EF.Audit
//{

//    public class AuditContext : DbContext, IAuditableContext
//    {

//        //internal List<IAuditEntity> audits = new List<IAuditEntity>();

//        internal Stack<IAuditCommand> auditingList = new Stack<IAuditCommand>();

//        public AuditContext()
//        {

//        }

//        public void AddCommand(IAuditCommand c)
//        {
//            auditingList.Push(c);
//        }

//        public override int SaveChanges()
//        {
//            int result = 0;

//            foreach (var audit in auditingList)
//            {
//                audit.BeforeSave(this);
//            }

//            base.SaveChanges();



//            foreach (var audit in auditingList)
//            {
//                audit.AfterSave(this);
//            }

//            base.SaveChanges();

//            return result;
//        }
//    }

//    public interface IShadowAuditableEntity
//    {
//        [NotMapped]
//        Guid UniqueId { get; set; }


//        //string ObjectId { get; set; }
//    }
//    public interface IAuditCommand
//    {
//        void BeforeSave(AuditContext context);
//        void AfterSave(AuditContext context);

//    }
//    public class ShadowAuditing : IAuditCommand
//    {

//        private static Dictionary<Type, AuditTypeInfo> auditTypes = new Dictionary<Type, AuditTypeInfo>();

//        List<IShadowAuditableEntity> auditableEntities = new List<IShadowAuditableEntity>();

//        List<TempShadowClass> tempShadows = new List<TempShadowClass>();




//        public static bool AuditEnabled { get; private set; }

//        private const string CreatedColumnName = "Created";
//        private const string CreatedByColumnName = "CreatedBy";
//        private const string LogTypeColumnName = "LogType";
//        private const string ModifiedColumnName = "Modified";
//        private const string ModifiedByColumnName = "ModifiedBy";
//        private const string LogDateColumnName = "LogDate";
//        private const string ObjectIdColumnName = "ObjectId";
//        private const string UniqueIdColumnName = "UniqueId";
//        private const string ChannelColumnName = "Channel";



//        public static void RegisterAuditType<E, A, P>(Expression<Func<E, P>> primaryKeyName) where E : IShadowAuditableEntity where A : IAuditEntity
//        {


//            Type auditableEntityType = typeof(E);
//            Type auditEntityType = typeof(A);



//            if (auditTypes.ContainsKey(auditableEntityType))
//            {
//                throw new ArgumentException("Type already registered for auditing.", "auditableEntityType");
//            }

//            var iface = auditableEntityType.GetInterface("IShadowAuditableEntity");


//            if (iface == null)
//            {
//                throw new ArgumentException("Entity does implement IShadowAuditableEntity", "auditableEntityType");
//            }



//            if (auditEntityType != null)
//            {
//                //iface = auditEntityType.GetInterface("IAuditEntity");

//                //if (iface == null)
//                //{
//                //    throw new ArgumentException("Entity does implement IAuditEntity", "auditEntityType");
//                //}

//                AuditTypeInfo info = new AuditTypeInfo { AuditableEntityType = auditableEntityType, AuditEntityType = auditEntityType };


//                MemberExpression propertyBody = primaryKeyName.Body as MemberExpression;

//                info.PrimaryKeyName = propertyBody.Member.Name;

//                var properties = auditEntityType.GetProperties();

//                var entityProperties = auditableEntityType.GetProperties().ToDictionary(x => x.Name);

//                foreach (var property in properties)
//                {
//                    if (entityProperties.ContainsKey(property.Name))
//                    {
//                        if (property.PropertyType == entityProperties[property.Name].PropertyType)
//                        {
//                            info.AuditProperties.Add(property.Name);
//                        }
//                    }
//                }

//                auditTypes.Add(auditableEntityType, info);
//            }


//        }

//        //static void Init()
//        //{
//        //    AuditConfigurationSection config = ConfigurationManager.GetSection("entityFramework.Audit") as AuditConfigurationSection;

//        //    if (config == null)
//        //    {
//        //        config = new AuditConfigurationSection();
//        //    }

//        //    AuditEnabled = config.Enabled;

//        //    foreach (EntityElement item in config.Entities)
//        //    {
//        //        var entity = Type.GetType(item.Name);
//        //        var auditEntity = Type.GetType(item.Audit);

//        //        if (entity != null)
//        //        {
//        //            //RegisterAuditType(entity, auditEntity,null);
//        //        }
//        //    }
//        //}




//        public void BeforeSave(AuditContext context)
//        {
//            foreach (EntityEntry<IShadowAuditableEntity> auditable in context.ChangeTracker.Entries<IShadowAuditableEntity>())
//            {
//                if (auditable.State == EntityState.Added
//                    || auditable.State == EntityState.Modified
//                    || auditable.State == EntityState.Deleted)
//                {

//                    //if (auditable.State == EntityState.Modified || auditable.State == EntityState.Deleted)
//                    {
//                        Type entityType = auditable.Entity.GetType();

//                        if (entityType.Namespace.Contains("Entity.DynamicProxies"))
//                        {
//                            entityType = entityType.BaseType;
//                        }

//                        if (auditTypes.ContainsKey(entityType) && auditTypes[entityType].AuditEntityType != null)
//                        {

//                            //var auditEntity = this.CreateAuditEntity(auditable, auditTypes[entityType], context);

//                            var tempShadowClass = this.CreateTempShadowClass(auditable, auditTypes[entityType], context);
//                            this.tempShadows.Add(tempShadowClass);
//                            //auditEntities.Add(auditEntity);
//                            auditableEntities.Add(auditable.Entity);
//                        }
//                    }

//                }
//            }
//        }


//        public void AfterSave(AuditContext context)
//        {
//            foreach (var temp in tempShadows)
//            {

//                Type auditEntityType = temp.AuditTypeInfo.AuditEntityType;

//                if (auditEntityType.Namespace.Contains("Entity.DynamicProxies"))
//                {
//                    auditEntityType = auditEntityType.BaseType;
//                }


//                var auditableEntity = this.auditableEntities.Where(c => c.UniqueId == temp.UniqueId).SingleOrDefault();


//                string ObjectId = temp.AuditTypeInfo.AuditableEntityType.GetProperty(temp.AuditTypeInfo.PrimaryKeyName).GetValue(auditableEntity, null).ToString();

//                temp.ObjectId = ObjectId;

//                this.CreateAuditEntity(temp, temp.AuditTypeInfo, context);

//            }
//        }

//        private TempShadowClass CreateTempShadowClass(EntityEntry<IShadowAuditableEntity> entityEntry, AuditTypeInfo auditTypeInfo, DbContext context)
//        {
//            TempShadowClass shadowClass = new TempShadowClass();

//            shadowClass.AuditTypeInfo = auditTypeInfo;
//            shadowClass.State = entityEntry.State;

//            foreach (string propertyName in auditTypeInfo.AuditProperties)
//            {

//                if (propertyName == UniqueIdColumnName)
//                {
//                    shadowClass.UniqueId = entityEntry.Entity.UniqueId;
//                    continue;
//                }

//                if (entityEntry.State == EntityState.Added)
//                {
//                    //auditEntityEntry.Property(propertyName).CurrentValue = entityEntry.Property(propertyName).CurrentValue;

//                    shadowClass.Properties.Add(new TempShadowClassProperty
//                    {
//                        Value = entityEntry.Property(propertyName).CurrentValue,
//                        PropertyName = propertyName,
//                    });


//                }
//                else
//                {
//                    //auditEntityEntry.Property(propertyName).CurrentValue = entityEntry.Property(propertyName).OriginalValue;

//                    shadowClass.Properties.Add(new TempShadowClassProperty
//                    {
//                        Value = entityEntry.Property(propertyName).OriginalValue,
//                        PropertyName = propertyName,
//                    });
//                }
//            }

//            var UpdateDateTime = DateTime.Now; //context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();


//            ClaimsPrincipal principal = Thread.CurrentPrincipal as ClaimsPrincipal;

//            //auditEntityEntry.Property(CreatedColumnName).CurrentValue = UpdateDateTime;
//            //auditEntityEntry.Property(CreatedByColumnName).CurrentValue = principal.Identity.Name;
//            //auditEntityEntry.Property(ModifiedColumnName).CurrentValue = UpdateDateTime;
//            //auditEntityEntry.Property(ModifiedByColumnName).CurrentValue = principal.Identity.Name;
//            //auditEntityEntry.Property(LogDateColumnName).CurrentValue = UpdateDateTime;
//            //auditEntityEntry.Property(UniqueIdColumnName).CurrentValue = entityEntry.Entity.UniqueId;


//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = CreatedColumnName, Value = UpdateDateTime });
//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = CreatedByColumnName, Value = principal.Identity.Name });
//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = ModifiedColumnName, Value = UpdateDateTime });
//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = ModifiedByColumnName, Value = principal.Identity.Name });
//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = LogDateColumnName, Value = UpdateDateTime });
//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = UniqueIdColumnName, Value = entityEntry.Entity.UniqueId });





//            int logType = 0;

//            switch (entityEntry.State)
//            {
//                case EntityState.Detached:
//                    break;
//                case EntityState.Unchanged:
//                    break;
//                case EntityState.Added:
//                    logType = 4;
//                    break;
//                case EntityState.Deleted:
//                    logType = 8;
//                    break;
//                case EntityState.Modified:
//                    logType = 16;
//                    break;
//                default:
//                    break;
//            }


//            //auditEntityEntry.Property(LogTypeColumnName).CurrentValue = logType;



//            shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = LogTypeColumnName, Value = logType });


//            var channelId = principal.Claims.Where(c => c.Type == ChannelColumnName).SingleOrDefault();

//            if (channelId != null)
//            {
//                //auditEntityEntry.Property(ChannelColumnName).CurrentValue = channelId.Value;
//                shadowClass.Properties.Add(new TempShadowClassProperty { PropertyName = ChannelColumnName, Value = channelId.Value });

//            }

//            return shadowClass;

//        }

//        private IAuditEntity CreateAuditEntity(TempShadowClass temp, AuditTypeInfo auditTypeInfo, AuditContext context)
//        {
            

//            DbSet set = context.Set(auditTypeInfo.AuditEntityType);

//            IAuditEntity auditEntity = set.Create() as IAuditEntity;

//            set.Add(auditEntity);

//            EntityEntry auditEntityEntry = context.Entry(auditEntity);


//            foreach (string propertyName in auditTypeInfo.AuditProperties)
//            {

//                auditEntityEntry.Property(propertyName).CurrentValue = temp.Properties.Single(p => p.PropertyName == propertyName).Value;
//            }


//            ClaimsPrincipal principal = Thread.CurrentPrincipal as ClaimsPrincipal;

//            auditEntityEntry.Property(CreatedColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == CreatedColumnName).Value;
//            auditEntityEntry.Property(CreatedByColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == CreatedByColumnName).Value;
//            auditEntityEntry.Property(ModifiedColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == ModifiedColumnName).Value;
//            auditEntityEntry.Property(ModifiedByColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == ModifiedByColumnName).Value;
//            auditEntityEntry.Property(LogDateColumnName).CurrentValue = temp.Properties.Single(p => p.PropertyName == LogDateColumnName).Value;
//            auditEntityEntry.Property(UniqueIdColumnName).CurrentValue = temp.UniqueId;
//            auditEntityEntry.Property(ObjectIdColumnName).CurrentValue = temp.ObjectId;


//            int logType = 0;

//            switch (temp.State)
//            {
//                case EntityState.Detached:
//                    break;
//                case EntityState.Unchanged:
//                    break;
//                case EntityState.Added:
//                    logType = 4;
//                    break;
//                case EntityState.Deleted:
//                    logType = 8;
//                    break;
//                case EntityState.Modified:
//                    logType = 16;
//                    break;
//                default:
//                    break;
//            }


//            auditEntityEntry.Property(LogTypeColumnName).CurrentValue = logType;

//            var channelId = principal.Claims.Where(c => c.Type == ChannelColumnName).SingleOrDefault();

//            if (channelId != null)
//            {
//                auditEntityEntry.Property(ChannelColumnName).CurrentValue = channelId.Value;
//            }

//            return auditEntity;
//        }


//    }


//    public class TempShadowClassProperty
//    {
//        public object Value { get; set; }

//        public string PropertyName { get; set; }
//    }

//    public class TempShadowClass
//    {

//        public TempShadowClass()
//        {
//            this.Properties = new List<TempShadowClassProperty>();
//        }
//        public EntityState State { get; set; }

//        public List<TempShadowClassProperty> Properties { get; set; }


//        public AuditTypeInfo AuditTypeInfo { get; set; }

//        public Guid UniqueId { get; set; }

//        public string ObjectId { get; set; }

//    }
//}
