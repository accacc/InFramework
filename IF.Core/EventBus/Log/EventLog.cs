using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.EventBus.Log
{
    public class EventLog
    {
        public int Id { get; set; }
        public Guid EventId { get; set; }
        public Guid SourceId { get; set; }
        public string EventTypeName { get; set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }

        public string ServiceName { get; set; }
    }
}
