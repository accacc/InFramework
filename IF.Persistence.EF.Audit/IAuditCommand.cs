using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Audit
{
    public interface IAuditCommand
    {
        void BeforeSave(AuditContext context);
        void AfterSave(AuditContext context);

    }
}
