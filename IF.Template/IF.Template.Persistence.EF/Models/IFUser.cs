using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Persistence.EF.Models
{
    public class IFUser: IF.Core.Data.Entity
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
