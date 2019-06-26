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
//    public class SelfAuditing : IAuditCommand
//    {
//        private const string CreatedColumnName = "Created";
//        private const string CreatedByColumnName = "CreatedBy";
//        private const string LogTypeColumnName = "LogType";
//        private const string ModifiedColumnName = "Modified";
//        private const string ModifiedByColumnName = "Modified";
//        private const string LogDateColumnName = "LogDate";

//        public void Audit(AuditContext context)
//        {

//            var UpdateDateTime = context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();

//            foreach (DbEntityEntry<ISelfAuditableEntity> auditable in context.ChangeTracker.Entries<ISelfAuditableEntity>())
//            {

//                //if (auditable.State == EntityState.Modified)
//                //{
//                //    auditable.Entity.LastUpdateDate = context.UpdateDateTime;
//                //}
//                //else if (auditable.State == EntityState.Added)
//                //{
//                //    auditable.Entity.InsertDate = context.UpdateDateTime;
//                //    auditable.Entity.LastUpdateDate = null;
//                //}

//                if (auditable.State == EntityState.Modified)
//                {
//                    auditable.Entity.Modified = UpdateDateTime;
//                    auditable.Entity.ModifiedBy = context.UserName;
//                    auditable.Entity.Created = auditable.OriginalValues.GetValue<DateTime>(CreatedColumnName);
//                    auditable.Entity.CreatedBy = auditable.OriginalValues.GetValue<string>(CreatedByColumnName);
//                }
//                else if (auditable.State == EntityState.Added)
//                {
//                    auditable.Entity.Created = UpdateDateTime;
//                    auditable.Entity.CreatedBy = context.UserName;
//                    auditable.Entity.Modified = null;
//                    auditable.Entity.ModifiedBy = null;
//                }
//            }

//        }
//    }
//}
