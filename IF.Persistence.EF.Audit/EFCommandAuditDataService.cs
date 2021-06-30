//using IF.Core.Data;
//using IF.Core.Log;

//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace IF.Persistence.EF.Audit
//{
//    public class EFCommandAuditDataService : GenericRepository,ICommandAuditDataService
//    {

//        public EFCommandAuditDataService()
//        {

//        }
//        public Task<AuditLog> GetDetailAsync(Guid uniqueId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<PagedListResponse<AuditLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string Source, string UserId, int PageNumber = 0, int PageSize = 50)
//        {
//            throw new NotImplementedException();
//        }

//        public void Log(object @object, Guid uniqueId, DateTime LogDate, string objectName, string IpAdress, string Channel, string UserId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task LogAsync(object @object, Guid uniqueId, DateTime LogDate, string objectName, string IpAdress, string Channel, string UserId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
