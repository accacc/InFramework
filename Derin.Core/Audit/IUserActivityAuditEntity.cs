using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Audit
{
    public interface IUserActivityAuditEntity : IAuditEntity
    {
         int ID { get; set; }
         string ENTITY_NAME { get; set; }
         string ENTITY_ID { get; set; }
         string OPERATION_TYPE { get; set; }
         string USER_ID { get; set; }
         System.DateTime OPERATION_DATE { get; set; }
    }
}
