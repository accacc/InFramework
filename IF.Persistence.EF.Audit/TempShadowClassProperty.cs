using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Audit
{
    public class TempShadowClassProperty
    {
        public object Value { get; set; }

        public string PropertyName { get; set; }

        
    }
}
