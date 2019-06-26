using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Navigation.Persistence.EF.Model
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Action Name")]
        [Required]
        [StringLength(50)]
        public string ActionName { get; set; }

        [Display(Name = "Controller Name")]
        [Required]
        [StringLength(50)]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Display(Name = "Resource Key")]
        [Required]
        [StringLength(100)]
        public string ResourceKey { get; set; }

        [Display(Name = "Icon Name")]
        [StringLength(20)]
        public string IconName { get; set; }

        [Display(Name = "Route Parameter")]
        [StringLength(100)]
        public string RouteParameter { get; set; }

        [StringLength(10)]
        public string Method { get; set; }

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
            var x = obj as Permission;
            if (x == null) return false;
            if (Id == 0 && x.Id == 0) return ReferenceEquals(this, x);
            return (Id == x.Id);

        }
        #endregion
    }
}
