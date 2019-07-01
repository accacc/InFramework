using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.EventBus.Log
{
    public interface  IEventLog:IIFSystemTable

    {

        int Id { get; set; }
        Guid EventId { get; set; }
        Guid SourceId { get; set; }
        string EventTypeName { get; set; }
        EventStateEnum State { get; set; }
        int TimesSent { get; set; }
        DateTime CreationTime { get; set; }
        string Content { get; set; }

        string ServiceName { get; set; }
    }

    public class EventLog: IEventLog
    {
        public int Id { get; set; }
        public Guid EventId { get; set; }
        public Guid SourceId { get; set; }
        public string EventTypeName { get; set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }

        public Guid UniqueId { get; set; }

        public string ServiceName { get; set; }
    }
}
