
using System;
namespace IF.Core.Audit
{
    public interface ISimpleAuditEntity : IAuditEntity
    {
        long Id { get; set; }
        string Type { get; set; }
        string TableSchema { get; set; }
        string TableName { get; set; }
        string PrimaryKey { get; set; }
        string FieldName { get; set; }
        string OldValue { get; set; }
        string NewValue { get; set; }
        Nullable<System.DateTime> Date { get; set; }
        string UserId { get; set; }
    }
}
