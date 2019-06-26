using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Navigation.Persistence.EF.Model
{
    public class PermissionMap
    {

        public PermissionMap()
        {
            PermissionMaps = new List<PermissionMap>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Permission Id")]
        [Required]
        public int PermissionId { get; set; }

        [Display(Name = "Sort Order")]
        [Required]
        public short SortOrder { get; set; }

        [Required]
        public short Type { get; set; }

        [Display(Name = "Widget Type")]
        [Required]
        public short WidgetType { get; set; }

        [Display(Name = "Widget Render Type")]
        [Required]
        public short WidgetRenderType { get; set; }

        [Display(Name = "Is Active")]
        [Required]
        public bool IsActive { get; set; }

        public virtual PermissionMap Parent { get; set; }

        public virtual ICollection<PermissionMap> PermissionMaps { get; private set; }

        #region overrides

        public override string ToString()
        {
            return "[Id] = " + Id;

        }

        public override int GetHashCode()
        {
            if (Id == 0) return base.GetHashCode(); //transient instance
            return Id;

        }

        public override bool Equals(object obj)
        {
            var x = obj as PermissionMap;
            if (x == null) return false;
            if (Id == 0 && x.Id == 0) return ReferenceEquals(this, x);
            return (Id == x.Id);

        }
        #endregion
    }
}
