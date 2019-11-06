using IF.Core.EventBus;
using System;

namespace IF.Batch
{
    public class IFBatchEvent : IFEvent
    {
        public string BatchName { get; set; }

        public int BatchNumber { get; set; }

        public int BatchCount { get; set; }


        public string BulkName { get; set; }

        public IFBatchEvent(string BulkName, Guid sourceId) : base(sourceId)
        {
            this.BulkName = BulkName;
        }
    }
}
