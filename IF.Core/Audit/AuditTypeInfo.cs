using System;
using System.Collections.ObjectModel;

namespace IF.Core.Audit
{
    public class AuditTypeInfo
    {
        public AuditTypeInfo()
        {
            this.AuditProperties = new Collection<string>();
        }


        public string PrimaryKeyName { get; set; }

        public Type AuditableEntityType { get; set; }

        public Type AuditEntityType { get; set; }

        public Collection<string> AuditProperties { get; set; }
    }
}
