using IF.Core.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Persistence.EF.Audit
{

   
    public abstract class AuditEntity : Entity, IAuditEntity
    {

        public AuditEntity()
        {
            
        }

        [NotMapped]
        public Guid UniqueId { get; set; }

        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public int LogType { get; set; }
        public DateTime? LogDate { get; set; }

        public string ObjectId { get; set; }

        public string Channel { get; set; }
    }

    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

    }
}
