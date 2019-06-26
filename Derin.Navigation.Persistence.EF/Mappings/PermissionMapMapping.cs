using Derin.Navigation.Persistence.EF.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Mapping
{

    /// <summary>
    /// Class mapping to PermissionMap table
    /// </summary>
    public class PermissionMapMapping : EntityTypeConfiguration<PermissionMap>
    {

        /// <summary>
        /// Mapping
        /// </summary>
        public PermissionMapMapping()
        {
            //table
            ToTable("PermissionMap");
            // Properties
            //  Id is primary key (identity)
            Property(x => x.Id).IsRequired();
            Property(x => x.PermissionId).IsRequired();
            Property(x => x.SortOrder).IsRequired();
            Property(x => x.Type).IsRequired();
            Property(x => x.WidgetType).IsRequired();
            Property(x => x.WidgetRenderType).IsRequired();
            Property(x => x.IsActive).IsRequired();
            HasOptional(x => x.Parent).WithMany(c => c.PermissionMaps);
            // Navigation properties
            //Foreign key to PermissionMap (PermissionMap)
            HasMany(x => x.PermissionMaps);
        }
    }
}
