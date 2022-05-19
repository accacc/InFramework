using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

using Newtonsoft.Json;

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
            var auditEntries = new List<AuditLog>();


            foreach (var temp in tempShadows)
            {

                var auditEntry = temp.AuditEntry;


                Dictionary<string, object> KeyValues = new Dictionary<string, object>();



                foreach (var property in temp.Entity.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                }

                auditEntry.PrimaryKey = JsonConvert.SerializeObject(KeyValues);

                auditEntries.Add(auditEntry);
            }


            foreach (var auditEntry in auditEntries)
            {
                context.DbContext.Add(auditEntry);
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

            var auditEntries = new List<AuditLog>();


            foreach (EntityEntry<ISimpleAuditableEntity> entry in context.DbContext.ChangeTracker.Entries<ISimpleAuditableEntity>())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;


                var auditEntry = new AuditLog();
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);

                Dictionary<string, object> OldValues = new Dictionary<string, object>();
                Dictionary<string, object> NewValues = new Dictionary<string, object>();
                List<string> ChangedColumns = new List<string>();


                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    //if (property.Metadata.IsPrimaryKey())
                    //{
                    //    this.KeyValues[propertyName] = property.CurrentValue;
                    //    continue;
                    //}

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.Type = AuditType.Create;
                            NewValues[propertyName] = property.CurrentValue;
                            auditEntry.CreatedBy = "1";
                            auditEntry.Created = DateTime.Now;
                            break;
                        case EntityState.Deleted:
                            auditEntry.Type = AuditType.Delete;
                            OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:

                            if (property.IsModified)
                            {
                                ChangedColumns.Add(propertyName);
                                auditEntry.Type = AuditType.Update;
                                auditEntry.ModifiedBy = "1";
                                auditEntry.Modified = DateTime.Now;
                                OldValues[propertyName] = property.OriginalValue;
                                NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }


                auditEntry.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
                auditEntry.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
                auditEntry.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);

                var UpdateDateTime = DateTime.Now; //context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();


                ClaimsPrincipal principal = Thread.CurrentPrincipal as ClaimsPrincipal;



                string identityName = "";

                if (principal != null && principal.Identity != null)
                {
                    identityName = principal.Identity.Name;
                }

                //auditEntry.UserId = identityName;

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
