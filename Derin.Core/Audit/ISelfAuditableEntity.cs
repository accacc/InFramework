using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Audit
{
    public interface  ISelfAuditableEntity:IAuditableEntity
    {
        DateTime? Modified { get; set; }
        DateTime Created { get; set; }
        int? ModifiedBy { get; set; }
        int CreatedBy { get; set; }
    }
}
