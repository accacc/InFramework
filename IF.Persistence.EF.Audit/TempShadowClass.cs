using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Audit
{
    public class TempShadowClass
    {

        public TempShadowClass()
        {
            this.Properties = new List<TempShadowClassProperty>();
        }
        public EntityState State { get; set; }

        public List<TempShadowClassProperty> Properties { get; set; }


        public AuditTypeInfo AuditTypeInfo { get; set; }

        public Guid UniqueId { get; set; }

        public string ObjectId { get; set; }

    }
}
