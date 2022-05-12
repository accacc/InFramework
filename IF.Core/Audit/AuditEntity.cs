using IF.Core.Data;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace IF.Core.Audit
{

    public class Audit:Entity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }

        public DateTime? Modified { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
    public class AuditEntity
    {
        public AuditEntity()
        {
        }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.CreatedBy = UserId;
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.Created = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
