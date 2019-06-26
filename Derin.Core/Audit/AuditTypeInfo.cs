using System;
using System.Collections.ObjectModel;

namespace Derin.Core.Audit
{
    public class AuditTypeInfo
    {
        public AuditTypeInfo()
        {
            this.AuditProperties = new Collection<string>();
        }

        public Type EntityType { get; set; }

        public Type AuditEntityType { get; set; }

        public Collection<string> AuditProperties { get; set; }
    }
}
