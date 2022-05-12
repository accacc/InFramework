using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;

namespace IF.Persistence.EF.Audit
{

    public class TempSimpleAuditClass
    {

        public EntityEntry<ISimpleAuditableEntity> Entity { get; set; }

            public EntityState State { get; set; }

        public AuditEntity AuditEntry { get; set; }


    }

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

        public EntityEntry<IShadowAuditableEntity> Entity { get; set; }

    }
}
