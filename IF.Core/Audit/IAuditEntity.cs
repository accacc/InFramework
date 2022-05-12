using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IF.Core.Audit
{
    public interface IAuditEntity
    {
        DateTime? Modified { get; set; }
        DateTime Created { get; set; }
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }
}
