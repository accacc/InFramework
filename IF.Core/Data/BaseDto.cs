using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public class BaseDto
    {
        public int Id { get; set; }
    }

    public class BaseJsonDto
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}
