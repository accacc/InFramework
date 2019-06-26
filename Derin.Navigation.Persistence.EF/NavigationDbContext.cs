using Derin.Navigation.Persistence.EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Navigation.Persistence.EF
{

    public class NavigationDbContext : DbContext
    {

        public NavigationDbContext(string cnnStringName) : base(cnnStringName)
        {
            Database.SetInitializer<NavigationDbContext>(null);

        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionMap> PermissionMaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
