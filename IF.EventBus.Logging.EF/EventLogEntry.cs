using IF.Core.EventBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.EventBus.Logging.EF
{
    public class EventLogEntry
    {
        private EventLogEntry() { }
        public EventLogEntry(IFEvent @event)
        {
            EventId = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.Publishing;
            TimesSent = 0;
            this.SourceId = @event.SourceId;
        }

        public int Id { get; set; }
        public Guid EventId { get; private set; }
        public Guid SourceId { get; private set; }
        public string EventTypeName { get; private set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }

        public string ServiceName { get; set; }
    }
}
