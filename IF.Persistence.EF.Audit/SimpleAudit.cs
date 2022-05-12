using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace IF.Persistence.EF.Audit
{
    public class SimpleAudit : IAuditCommand
    {

        public static Type SimpleAuditTableType;

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

        List<TempSimpleAuditClass> tempShadows = new List<TempSimpleAuditClass>();

        List<ISimpleAuditableEntity> auditableEntities = new List<ISimpleAuditableEntity>();

        public void AfterSave(AuditContext context)
        {
            var auditEntries = new List<AuditEntity>();


            foreach (var temp in tempShadows)
            {

                var auditEntry = temp.AuditEntry;

                foreach (var property in temp.Entity.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                }

                auditEntries.Add(auditEntry);
            }


            foreach (var auditEntry in auditEntries)
            {
                context.DbContext.Add(auditEntry.ToAudit());
            }
        }


        public void BeforeSave(AuditContext context)
        {
            foreach (EntityEntry<ISimpleAuditableEntity> auditable in context.DbContext.ChangeTracker.Entries<ISimpleAuditableEntity>())
            {
                if (auditable.State == EntityState.Added
                    || auditable.State == EntityState.Modified
                    || auditable.State == EntityState.Deleted)
                {



                    var tempShadowClass = this.CreateTempShadowClass(auditable, context);

                    this.tempShadows.Add(tempShadowClass);

                    auditableEntities.Add(auditable.Entity);
                }

            }
        }



        private TempSimpleAuditClass CreateTempShadowClass(EntityEntry<ISimpleAuditableEntity> entityEntry, AuditContext context)
        {
            TempSimpleAuditClass shadowClass = new TempSimpleAuditClass();

            shadowClass.Entity = entityEntry;

            shadowClass.State = entityEntry.State;

            var auditEntries = new List<AuditEntity>();


            foreach (EntityEntry<ISimpleAuditableEntity> entry in context.DbContext.ChangeTracker.Entries<ISimpleAuditableEntity>())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;


                var auditEntry = new AuditEntity();
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = "a";
                auditEntries.Add(auditEntry);


                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }


                var UpdateDateTime = DateTime.Now; //context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();


                ClaimsPrincipal principal = Thread.CurrentPrincipal as ClaimsPrincipal;



                string identityName = "";

                if (principal != null && principal.Identity != null)
                {
                    identityName = principal.Identity.Name;
                }

                auditEntry.UserId = identityName;

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


                if (principal != null)
                {

                    var channelId = principal.Claims.Where(c => c.Type == ChannelColumnName).SingleOrDefault();

                    if (channelId != null)
                    {

                    }

                }

                shadowClass.AuditEntry = auditEntry;
            }



            return shadowClass;

        }
    }
}
