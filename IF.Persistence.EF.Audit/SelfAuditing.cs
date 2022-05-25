using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Audit
{
    public class SelfAuditing : IAuditCommand
    {
        public void AfterSave(AuditContext context)
        {
            
        }

        

        public void BeforeSave(AuditContext context)
        {
            foreach (EntityEntry<ISelfAuditableEntity> auditable in context.DbContext.ChangeTracker.Entries<ISelfAuditableEntity>())
            {

                if (auditable.State == EntityState.Modified)
                {
                    auditable.Entity.Modified = DateTime.Now;
                    auditable.Entity.ModifiedBy = "user";
                }
                else if (auditable.State == EntityState.Added)
                {
                    auditable.Entity.Created = DateTime.Now;
                    auditable.Entity.ModifiedBy = "user";
                }
            }
        }
    }
}
