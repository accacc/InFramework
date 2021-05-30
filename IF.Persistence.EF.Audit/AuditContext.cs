using IF.Core.Audit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Persistence.EF.Audit
{

    public class AuditContext : IAuditableContext
    {
        internal DbContext DbContext;

        //internal List<IAuditEntity> audits = new List<IAuditEntity>();

        internal Stack<IAuditCommand> auditingList = new Stack<IAuditCommand>();

        public AuditContext(DbContext dbContext)
        {
            this.DbContext = dbContext;

        }



        public void AddCommand(IAuditCommand c)
        {
            auditingList.Push(c);
        }

        public int SaveChanges()
        {
            int result = 0;

            foreach (var audit in auditingList)
            {
                audit.BeforeSave(this);
            }

            this.DbContext.SaveChanges();



            foreach (var audit in auditingList)
            {
                audit.AfterSave(this);
            }

            this.DbContext.SaveChanges();

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = 0;

            foreach (var audit in auditingList)
            {
                audit.BeforeSave(this);
            }

            await this.DbContext.SaveChangesAsync();



            foreach (var audit in auditingList)
            {
                audit.AfterSave(this);
            }

            await this.DbContext.SaveChangesAsync();

            return result;
        }
    }


    //public class AuditContext : DbContext, IAuditableContext
    //{

    //    //internal List<IAuditEntity> audits = new List<IAuditEntity>();

    //    internal Stack<IAuditCommand> auditingList = new Stack<IAuditCommand>();

    //    public AuditContext()
    //    {

    //    }

    //    public AuditContext(DbContextOptions options) : base(options)
    //    {


    //    }

    //    public void AddCommand(IAuditCommand c)
    //    {
    //        auditingList.Push(c);
    //    }

    //    public override int SaveChanges()
    //    {
    //        int result = 0;

    //        foreach (var audit in auditingList)
    //        {
    //            audit.BeforeSave(this);
    //        }

    //        base.SaveChanges();



    //        foreach (var audit in auditingList)
    //        {
    //            audit.AfterSave(this);
    //        }

    //        base.SaveChanges();

    //        return result;
    //    }
    //}
}
