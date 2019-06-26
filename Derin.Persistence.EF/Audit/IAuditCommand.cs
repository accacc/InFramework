using Derin.Persistence.EF.Audit;
using IF.Core.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Persistence.EF
{
    public interface IAuditCommand
    {
        void BeforeSave(AuditContext context);
        void AfterSave(AuditContext context);

    }
}
