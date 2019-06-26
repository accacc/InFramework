//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Metadata.Edm;
//using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IF.Core.Audit;

//namespace Derin.Persistence.EF
//{
//    public class UserActivitiesAuditing : IAuditCommand
//    {


//        public string ID = "ID";
//        public string ENTITY_NAME = "ENTITY_NAME";
//        public string ENTITY_ID = "ENTITY_ID";
//        public string OPERATION_TYPE = "OPERATION_TYPE";
//        public string USER_ID = "USER_ID";
//        public string OPERATION_DATE = "OPERATION_DATE";

//        public static Type UserActivityTableType;

//        public void Audit(AuditContext context)
//        {
//            if (UserActivityTableType == null)
//            {
//                throw new ArgumentNullException("please set UserActivityTableType");
//            }

//            var UpdateDateTime = context.Database.SqlQuery<DateTime>("select getdate()", new object[] { }).First();

//            foreach (DbEntityEntry entity in context.ChangeTracker.Entries())
//            {
//                if (entity.State == EntityState.Added
//                    || entity.State == EntityState.Modified
//                    || entity.State == EntityState.Deleted)
//                {



//                    IUserActivityAuditEntity activity = context.CreateEntity<IUserActivityAuditEntity>(UserActivityTableType);

//                    activity.OPERATION_DATE = UpdateDateTime;
//                    activity.OPERATION_TYPE = entity.State.ToString();
//                    activity.USER_ID = context.UserName.ToString();
//                    activity.ENTITY_NAME = context.GetTableName(entity);
//                    activity.ENTITY_ID = context.GetPrimaryKeyValue(entity).ToString();
//                }

//            }

//        }

//    }

//}