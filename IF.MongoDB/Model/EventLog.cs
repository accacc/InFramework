using IF.Core.EventBus;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Model
{
    public class EventLogMondoDb
    {

        private EventLogMondoDb() { }
        public EventLogMondoDb(IFEvent @event)
        {
            EventId = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.Publishing;
            TimesSent = 0;
            this.SourceId = @event.SourceId;
        }

        [BsonId]
        public ObjectId InternalId { get; set; }

        public Guid EventId { get; private set; }
        public Guid SourceId { get; private set; }
        public string EventTypeName { get; private set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }

        public string ServiceName { get; set; }




      

      
    }
}
