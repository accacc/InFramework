//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IF.Core.Audit;

//namespace Derin.Persistence.EF
//{
//    public class SimpleAuditing : IAuditCommand
//    {

//        public static Type SimpleAuditTableType;

//        public void Audit(AuditContext context)
//        {

//            if (SimpleAuditTableType == null)
//            {
//                throw new ArgumentNullException("please set SimpleAuditTableType");
//            }

//            var UpdateDateTime = context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();


//            foreach (DbEntityEntry<ISimpleAuditableEntity> dbEntry in context.ChangeTracker.Entries<ISimpleAuditableEntity>())
//            {

//                List<ISimpleAuditEntity> result = new List<ISimpleAuditEntity>();

//                string tableName = context.GetTableName(dbEntry); ;
//                string keyValue = context.GetPrimaryKeyValue(dbEntry).ToString();


//                if (dbEntry.State == EntityState.Added)
//                {
//                    ISimpleAuditEntity log = context.CreateEntity<ISimpleAuditEntity>(SimpleAuditTableType);

//                    log.UserId = context.UserName.ToString();
//                    log.Date = UpdateDateTime;
//                    log.Type = "A";
//                    log.TableName = tableName;
//                    log.PrimaryKey = keyValue;
//                    log.FieldName = "*ALL";
//                    log.NewValue = dbEntry.CurrentValues.ToObject().ToString();
//                    result.Add(log);
//                }
//                else if (dbEntry.State == EntityState.Deleted)
//                {
//                    ISimpleAuditEntity log = context.CreateEntity<ISimpleAuditEntity>(SimpleAuditTableType);
//                    log.UserId = context.UserName.ToString();
//                    log.Date = UpdateDateTime;
//                    log.Type = "D";
//                    log.TableName = tableName;
//                    log.PrimaryKey = keyValue;
//                    log.FieldName = "*ALL";
//                    log.NewValue = dbEntry.OriginalValues.ToObject().ToString();
//                    result.Add(log);

//                }
//                else if (dbEntry.State == EntityState.Modified)
//                {
//                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
//                    {
//                        if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
//                        {

//                            ISimpleAuditEntity log = context.CreateEntity<ISimpleAuditEntity>(SimpleAuditTableType);
//                            log.UserId = context.UserName.ToString();
//                            log.Date = UpdateDateTime;
//                            log.Type = "M";
//                            log.TableName = tableName;
//                            log.PrimaryKey = keyValue;
//                            log.FieldName = propertyName;
//                            log.NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
//                            log.OldValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
//                            result.Add(log);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
