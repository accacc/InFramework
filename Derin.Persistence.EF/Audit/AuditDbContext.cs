using Derin.Persistence.EF.Audit;
using IF.Core.Audit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Derin.Persistence.EF
{

    public class AuditContext : DbContext, IAuditableContext
    {

        //internal List<IAuditEntity> audits = new List<IAuditEntity>();

        internal Stack<IAuditCommand> auditingList = new Stack<IAuditCommand>();

        public AuditContext()
        {
            
        }

        public void AddCommand(IAuditCommand c)
        {
            auditingList.Push(c);
        }

        public override int SaveChanges()
        {
            int result = 0;

            foreach (var audit in auditingList)
            {
                audit.BeforeSave(this);
            }

            base.SaveChanges();



            foreach (var audit in auditingList)
            {
                audit.AfterSave(this);
            }

            base.SaveChanges();

            return result;
        }
    }
}