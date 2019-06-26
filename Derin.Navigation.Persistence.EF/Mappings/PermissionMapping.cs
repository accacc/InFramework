using Derin.Navigation.Persistence.EF.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Mapping
{

    /// <summary>
    /// Class mapping to Permission table
    /// </summary>
    public class PermissionMapping : EntityTypeConfiguration<Permission>
    {

        /// <summary>
        /// Mapping
        /// </summary>
        public PermissionMapping()
        {
            //table
            ToTable("Permission");
            // Properties
            //  Id is primary key (identity)
            Property(x => x.Id).IsRequired();
            Property(x => x.ActionName).HasMaxLength(50).IsRequired();
            Property(x => x.ControllerName).HasMaxLength(50).IsRequired();
            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Text).HasMaxLength(250);
            Property(x => x.Description).HasMaxLength(100);
            Property(x => x.ResourceKey).HasMaxLength(100).IsRequired();
            Property(x => x.IconName).HasMaxLength(20);
            Property(x => x.RouteParameter).HasMaxLength(100);
            Property(x => x.Method).HasMaxLength(10);
        }
    }
}
