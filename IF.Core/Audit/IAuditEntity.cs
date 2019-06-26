using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IF.Core.Audit
{
    public interface IAuditEntity
    {
        DateTime? Modified { get; set; }
        string ModifiedBy { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        int LogType { get; set; }
        DateTime? LogDate { get; set; }

        string ObjectId { get; set; }

        string Channel { get; set; }


        [NotMapped]
        Guid UniqueId { get; set; }
    }
}
