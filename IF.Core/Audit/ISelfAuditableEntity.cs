using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Audit
{
    public interface  ISelfAuditableEntity:IAuditableEntity
    {
        DateTime? Modified { get; set; }
        DateTime Created { get; set; }
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }
}
