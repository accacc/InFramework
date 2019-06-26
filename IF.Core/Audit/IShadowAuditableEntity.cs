using IF.Core.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Audit
{
    public interface IShadowAuditableEntity
    {
        [NotMapped]
        Guid UniqueId { get; set; }


        //string ObjectId { get; set; }
    }
}
