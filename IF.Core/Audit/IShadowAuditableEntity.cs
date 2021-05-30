using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IF.Core.Audit
{
    public interface IShadowAuditableEntity: IAuditableEntity
    {
        [NotMapped]
        Guid UniqueId { get; set; }
    }
}
