using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Persistence.EF.Models
{
    public class TestEntity: IF.Core.Data.Entity
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }
    }
}
